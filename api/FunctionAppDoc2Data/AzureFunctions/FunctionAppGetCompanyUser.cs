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
    public class FunctionAppGetCompanyUser
    {
        private readonly IValidatedTokenService _validatedTokenService;
        private readonly ICompanyUserRepository _companyUserRepository;

        public FunctionAppGetCompanyUser(IValidatedTokenService validatedTokenService, ICompanyUserRepository companyUserRepository)
        {
            _validatedTokenService = validatedTokenService;
            _companyUserRepository = companyUserRepository;
        }

        [FunctionName("FunctionAppGetCompanyUser")]
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
                    Data = await _companyUserRepository.GetAllCompanyUsers(tokenValidation.userId),
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

