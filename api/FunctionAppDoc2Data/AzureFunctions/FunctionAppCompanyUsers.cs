using FunctionAppDoc2Data.Models;
using FunctionAppDoc2Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.AzureFunctions
{
    public class FunctionAppCompanyUsers
    {
        private readonly IValidatedTokenService _validatedTokenService;
        public FunctionAppCompanyUsers(IValidatedTokenService validatedTokenService)
        {
            _validatedTokenService = validatedTokenService;
        }

        [FunctionName("FunctionAppCompanyUsers")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
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
                using StreamReader reader = new(req.Body);
                string bodyStr = await reader.ReadToEndAsync();
                var receiptMaster = JsonConvert.DeserializeObject<ExpenseTypeDTO>(bodyStr);

                return new OkObjectResult(new
                {
                    Data = "Data Successfully Added",
                    Message = "Success",
                    IsSuccess = true
                });
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
}
