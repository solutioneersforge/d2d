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
    public class FunctionAppUnitOfMeasure
    {
        private readonly IUnitOfMeasureRepository _unitOfMeasureRepository;
        private readonly IValidatedTokenService _validatedTokenService;
        public FunctionAppUnitOfMeasure(IUnitOfMeasureRepository unitOfMeasureRepository
            , IValidatedTokenService validatedTokenService)
        {
            _unitOfMeasureRepository = unitOfMeasureRepository;
            _validatedTokenService = validatedTokenService;
        }

        [FunctionName("FunctionAppUnitOfMeasure")]
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

                var resultData = await _unitOfMeasureRepository.GetUnitOfMeasuresAsync();
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
