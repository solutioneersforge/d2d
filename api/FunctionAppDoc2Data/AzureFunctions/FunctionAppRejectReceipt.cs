using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FunctionAppDoc2Data.Respositories;
using FunctionAppDoc2Data.Services;

namespace FunctionAppDoc2Data.AzureFunctions
{
    public class FunctionAppRejectReceipt
    {
        private readonly IExpenseSubExpenseRepository _expenseSubExpenseRepository;
        private readonly IValidatedTokenService _validatedTokenService;
        public FunctionAppRejectReceipt(IExpenseSubExpenseRepository expenseSubExpenseRepository
            , IValidatedTokenService validatedTokenService)
        {
            _expenseSubExpenseRepository = expenseSubExpenseRepository;
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

                var resultData = await _expenseSubExpenseRepository.GetExpenseSubExpenseCategoryAsync(tokenValidation.userId);
                return new OkObjectResult(new
                {
                    Data = resultData,
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
