using FunctionAppDoc2Data.Models;
using FunctionAppDoc2Data.Respositories;
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
    public class FunctionAppRejectReceipt
    {
        private readonly IRejectReceiptRepository _rejectReceiptRepository;
        private readonly IValidatedTokenService _validatedTokenService;
        public FunctionAppRejectReceipt(IRejectReceiptRepository rejectReceiptRepository
            , IValidatedTokenService validatedTokenService)
        {
            _rejectReceiptRepository = rejectReceiptRepository;
            _validatedTokenService = validatedTokenService;
        }

        [FunctionName("FunctionAppRejectReceipt")]
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

                var rejectReceiptDTO = JsonConvert.DeserializeObject<RejectReceiptDTO>(bodyStr);

                var resultData = await _rejectReceiptRepository.RejectReceipt(rejectReceiptDTO, tokenValidation.userId);
                if(resultData == 1)
                {
                    return new OkObjectResult(new
                    {
                        Data = "Successfully Reject the Receipt",
                        Message = "Success",
                        IsSuccess = true
                    });
                }
                else
                {
                    return new OkObjectResult(new
                    {
                        Data = "Your receipt request has been denied because you are either not authorized or the request to reject the receipt is invalid.",
                        Message = "Failed",
                        IsSuccess = false
                    });
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
}
