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
    public class FunctionAppCreateReceipt
    {
        private readonly IReceiptRespository _receiptRespository;
        private readonly IValidatedTokenService _validatedTokenService;
        public FunctionAppCreateReceipt(IReceiptRespository receiptRespository, IValidatedTokenService validatedTokenService)
        {
            _receiptRespository = receiptRespository;
            _validatedTokenService = validatedTokenService;
        }

        [FunctionName("FunctionAppCreateReceipt")]
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

                var file = req.Form.Files["file"];
                string filePath = string.Empty;
                if (file != null && file.Length > 0)
                {
                    filePath = await UploadImageToAzure.UploadImage(file);
                }

                using StreamReader reader = new(req.Body);
                string bodyStr = await reader.ReadToEndAsync();
                var receiptMasterStr = req.Form["receiptMasterDTO"];


                var receiptMaster = JsonConvert.DeserializeObject<ReceiptMasterDTO>(receiptMasterStr);
                receiptMaster.ImagePath = filePath;
                receiptMaster.OriginalFileName = file.FileName;
                receiptMaster.UserId = tokenValidation.userId;

                await _receiptRespository.CreateReceipt(receiptMaster);

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
