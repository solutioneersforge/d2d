using FunctionAppDoc2Data.Helpers;
using FunctionAppDoc2Data.Models;
using FunctionAppDoc2Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.AzureFunctions
{
    public class FunctionDoc2Data
    {
        private readonly IValidatedTokenService _validatedTokenService;
        public FunctionDoc2Data(IValidatedTokenService validatedTokenService)
        {
            _validatedTokenService = validatedTokenService;
        }
        [FunctionName("FunctionDoc2Data")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var tokenValidation = _validatedTokenService.ValidateTokenRequest(req);
            if (!tokenValidation.isSuccess)
            {
                return new OkObjectResult(new
                {
                    Data = "You are not authorized to access the application",
                    Message = "Failed",
                    IsSuccess = false
                });
            }

            log.LogInformation("C# HTTP trigger function processed a request.");
            var formCollection = await req.ReadFormAsync();
            HttpClient _httpClient = new HttpClient();
            if (formCollection != null)
            {
                using (var streamData = new MemoryStream())
                {
                    var file = formCollection.Files[0];
                    await file.CopyToAsync(streamData);
                    byte[] fileBytes = streamData.ToArray();
                    string base64String = Convert.ToBase64String(fileBytes);

                    var requestPayload = new
                    {
                        base64Source = base64String
                    };

                    string jsonPayload = System.Text.Json.JsonSerializer.Serialize(requestPayload);
                    HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");


                    _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Environment.GetEnvironmentVariable("OcpApimSubscriptionKey"));

                    try
                    {
                        HttpResponseMessage response = await _httpClient.PostAsync($"{Environment.GetEnvironmentVariable("PostCognitiveServices")}", content);

                        if (response.IsSuccessStatusCode)
                        {
                            string requestId = response.Headers.Contains("apim-request-id") ? response.Headers.GetValues("apim-request-id").FirstOrDefault() : "Not Found";

                            string responseBody = await response.Content.ReadAsStringAsync();

                            int maxRetryCount = 3;
                            int retryCount = 0;
                            string statusMessage = string.Empty;

                            HttpClient httpClient = new HttpClient();
                            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Environment.GetEnvironmentVariable("OcpApimSubscriptionKey"));

                            while (retryCount < maxRetryCount && statusMessage.ToLower() != "succeeded")
                            {
                                string getURL = string.Format(Environment.GetEnvironmentVariable("GetCognitiveServices"), requestId);
                                HttpResponseMessage responseGet = await httpClient.GetAsync($"{getURL}");
                                if (responseGet.IsSuccessStatusCode)
                                {
                                    string responseBodyGet = await responseGet.Content.ReadAsStringAsync();
                                    var data = System.Text.Json.JsonSerializer.Deserialize<Rootobject>(responseBodyGet);
                                    statusMessage = data.status;
                                    if (data.status.ToLower() == "succeeded")
                                    {
                                        return new OkObjectResult(new
                                        {
                                            ParseData = ReceiptItemParseData.GetReceiptDetails(data),
                                            Message = "File uploaded successfully",
                                            IsSuccess = true
                                        });
                                    }
                                    await Task.Delay(5000);
                                }
                                retryCount++;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return new OkObjectResult(new
                        {
                            Message = ex.Message.ToString(),
                            IsSuccess = false
                        });
                    }
                }
            }
            return new OkObjectResult(new
            {
                Message = "File uploaded failed",
                IsSuccess = false
            });
        }
    }
}
