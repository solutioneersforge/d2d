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
                using StreamReader reader = new(req.Body);
                string bodyStr = await reader.ReadToEndAsync();
                var userRegistration = JsonConvert.DeserializeObject<UserRegisterModelDTO>(bodyStr);
                Guid userId = Guid.Empty;
                if(String.IsNullOrEmpty(userRegistration.CompanyName))
                {
                    var tokenValidation = _validatedTokenService.ValidateTokenRequest(req);
                    userId = tokenValidation.userId;
                }
                  

                int result = await _appRegistrationRepository.CreateAppRegistration(userRegistration, userId);
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

                if (result == 5)
                {
                    return new OkObjectResult(new
                    {
                        Data = "A verification link has been successfully sent to your email address. Please click the link to activate your account.",
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
                        Data = $"The user email and password are invalid, or your email address has not been verified.",
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

        [FunctionName("FunctionAppUpdateUser")]
        public async Task<IActionResult> FunctionAppUpdateUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                using StreamReader reader = new(req.Body);
                string bodyStr = await reader.ReadToEndAsync();
                var userModel = JsonConvert.DeserializeObject<UserModelDTO>(bodyStr);
                
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

                int result = await _appRegistrationRepository.UpdateUserInformation(userModel, tokenValidation.userId);
                
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
                    Data = "Data Successfully Updated",
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
