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
    public class FunctionAppReceiptVerification
    {
        private readonly IReceiptVerificationRepository _receiptVerificationRepository;
        private readonly IValidatedTokenService _validatedTokenService;
        public FunctionAppReceiptVerification(IReceiptVerificationRepository receiptVerificationRepository
            , IValidatedTokenService validatedTokenService)
        {
            _receiptVerificationRepository = receiptVerificationRepository;
            _validatedTokenService = validatedTokenService;
        }

        [FunctionName("FunctionAppReceiptVerification")]
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

            Guid receiptId = Guid.Parse(req.Query["receiptId"]);
            try
            {
                return new OkObjectResult(new
                {
                    Data = _receiptVerificationRepository.GetReceiptVerification(receiptId),
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
