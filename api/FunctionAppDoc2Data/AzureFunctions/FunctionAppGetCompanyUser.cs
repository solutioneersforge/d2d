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


        [FunctionName("FunctionAppUpdateCompany")]
        public async Task<IActionResult> FunctionAppUpdateCompany(
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
                var companyUpdate = JsonConvert.DeserializeObject<CompanyUpdateDTO>(bodyStr);
                var result = await _companyUserRepository.UpdateCompanyDetails(tokenValidation.companyId, companyUpdate);
                
                if (result == 1)
                {
                    return new OkObjectResult(new
                    {
                        Data = "Company record successfully updated.",
                        Message = "Success",
                        IsSuccess = true
                    });
                }
                return new OkObjectResult(new
                {
                    Data = "Due to some technical issue we are unaable to process.",
                    Message = "Failed",
                    IsSuccess = false
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

