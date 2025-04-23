using FunctionAppDoc2Data.Respositories;
using FunctionAppDoc2Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using AuthorizationLevel = Microsoft.Azure.Functions.Worker.AuthorizationLevel;

namespace FunctionAppDoc2Data.AzureFunctions
{
    public class FunctionAppCategoryExpenseType
    {
        private readonly IExpenseSubExpenseRepository _expenseSubExpenseRepository;
        private readonly IValidatedTokenService _validatedTokenService;
        public FunctionAppCategoryExpenseType(IExpenseSubExpenseRepository expenseSubExpenseRepository
            , IValidatedTokenService validatedTokenService)
        {
            _expenseSubExpenseRepository = expenseSubExpenseRepository;
            _validatedTokenService = validatedTokenService;
        }

        [FunctionName("FunctionAppCategoryExpenseType")]
        public async Task<IActionResult> Run(
            [Microsoft.Azure.Functions.Worker.HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
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
