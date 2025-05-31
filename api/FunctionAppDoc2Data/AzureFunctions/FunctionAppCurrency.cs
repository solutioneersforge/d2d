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
    public class FunctionAppCurrency
    {
        private readonly ICurrencyTypeRepository _currencyTypeRepository;
        private readonly IValidatedTokenService _validatedTokenService;
        public FunctionAppCurrency(ICurrencyTypeRepository currencyTypeRepository
            , IValidatedTokenService validatedTokenService)
        {
            _currencyTypeRepository = currencyTypeRepository;
            _validatedTokenService = validatedTokenService;
        }

        [FunctionName("FunctionAppCurrency")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                var resultData = await _currencyTypeRepository.GetCurrencyTypeAsync();
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
