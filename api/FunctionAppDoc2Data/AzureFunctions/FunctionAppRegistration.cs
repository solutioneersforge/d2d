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
    public class FunctionAppRegistration
    {
        private readonly IAppRegistrationRepository _appRegistrationRepository;
        private readonly IAuthService _authService;
        private readonly IValidatedTokenService _validatedTokenService;
        public FunctionAppRegistration(IAppRegistrationRepository appRegistrationRepository
                , IAuthService authService, IValidatedTokenService validatedTokenService)
        {
            _appRegistrationRepository = appRegistrationRepository;
            _authService = authService;
            _validatedTokenService = validatedTokenService;
        }

        [FunctionName("FunctionAppRegistration")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                var tokenValidation = _validatedTokenService.ValidateTokenRequest(req);
                using StreamReader reader = new(req.Body);
                string bodyStr = await reader.ReadToEndAsync();
                var userRegistration = JsonConvert.DeserializeObject<UserRegisterModelDTO>(bodyStr);

                int result = await _appRegistrationRepository.CreateAppRegistration(userRegistration, tokenValidation.userId);
                if (result == -3)
                {
                    return new OkObjectResult(new
                    {
                        Data = $"Company Name {userRegistration.CompanyName} already exists.",
                        Message = "Failed",
                        IsSuccess = false
                    });
                }
                if (result == -1)
                {
                    return new OkObjectResult(new
                    {
                        Data = $"Data is not successfully added due to {userRegistration.Email} already exists.",
                        Message = "Failed",
                        IsSuccess = false
                    });
                }

                if (result == -4)
                {
                    return new OkObjectResult(new
                    {
                        Data = $"Data was not successfully added because the maximum number of users for the company was exceeded",
                        Message = "Failed",
                        IsSuccess = false
                    });
                }

                if (result == 0)
                {
                    return new OkObjectResult(new
                    {
                        Data = "Data is not successfully added due to data already exists.",
                        Message = "Failed",
                        IsSuccess = false
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


        [FunctionName("FunctionAppUserValidate")]
        public async Task<IActionResult> FunctionAppUserValidate(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                using StreamReader reader = new(req.Body);
                string bodyStr = await reader.ReadToEndAsync();
                var userRegistration = JsonConvert.DeserializeObject<UserValidateModelDTO>(bodyStr);

                var result = await _appRegistrationRepository.ValidateUserAsync(userRegistration.Email, userRegistration.Password);
                if (result.isSuccess == false)
                {
                    return new OkObjectResult(new
                    {
                        Data = $"User email and password is invalid",
                        Message = "Failed",
                        IsSuccess = false
                    });
                }


                return new OkObjectResult(new
                {
                    Data = result.token,
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
