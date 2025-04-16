using FunctionAppDoc2Data.Enums;
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
    public class FunctionAppReceiptModification
    {
        private readonly IReceiptApprovalRepository _receiptApprovalRepository;
        private readonly IValidatedTokenService _validatedTokenService;
        public FunctionAppReceiptModification(IReceiptApprovalRepository receiptApprovalRepository
                , IValidatedTokenService validatedTokenService)
        {
            _receiptApprovalRepository = receiptApprovalRepository;
            _validatedTokenService = validatedTokenService;
        }

        [FunctionName("FunctionAppReceiptModification")]
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

                var receiptMaster = JsonConvert.DeserializeObject<ReceiptApprovalDTO>(bodyStr);
                receiptMaster.StatusId = (int)StatusEnum.OPEN;
                await _receiptApprovalRepository.CreateUpdateReceiptAndItems(receiptMaster);

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
