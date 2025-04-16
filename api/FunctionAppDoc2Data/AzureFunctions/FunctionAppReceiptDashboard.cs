using FunctionAppDoc2Data.Respositories;
using FunctionAppDoc2Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.AzureFunctions
{
    public class FunctionAppReceiptDashboard
    {
        private readonly IReceiptDashbboardRepository _receiptDashbboardRepository;
        private readonly IValidatedTokenService _validatedTokenService;
        public FunctionAppReceiptDashboard(IReceiptDashbboardRepository receiptDashbboardRepository, IValidatedTokenService validatedTokenService)
        {
            _receiptDashbboardRepository = receiptDashbboardRepository;
            _validatedTokenService = validatedTokenService;
        }

        [FunctionName("FunctionAppReceiptDashboard")]
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

                return new OkObjectResult(new
                {
                    Data = _receiptDashbboardRepository.GetReceiptDashboard(tokenValidation.userId),
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
