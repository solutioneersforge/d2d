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

    public class FunctionAppExpenseType
    {
        private readonly IExpenseTypeRepository _expenseTypeRepository;
        private readonly IValidatedTokenService _validatedTokenService;
        public FunctionAppExpenseType(IExpenseTypeRepository expenseTypeRepository, IValidatedTokenService validatedTokenService)
        {
            _expenseTypeRepository = expenseTypeRepository;
            _validatedTokenService = validatedTokenService;
        }

        [FunctionName("FunctionAppExpenseType")]
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

                int result = _expenseTypeRepository.UpsertExpenseCategoryAndSubcategory(receiptMaster, tokenValidation.userId);

                if (result == 0)
                {
                    return new OkObjectResult(new
                    {
                        Data = "Data is not successfully added due to data already exists.",
                        Message = "Failed",
                        IsSuccess = false
                    });
                }

                if (result == 3)
                {
                    return new OkObjectResult(new
                    {
                        Data = "Subcategory sucessfully updated.",
                        Message = "Success",
                        IsSuccess = true
                    });
                }

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
