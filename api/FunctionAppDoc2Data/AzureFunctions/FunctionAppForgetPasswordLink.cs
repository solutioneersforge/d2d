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
using FunctionAppDoc2Data.Models;

namespace FunctionAppDoc2Data.AzureFunctions
{
    public class FunctionAppForgetPasswordLink
    {
        private readonly IForgetPasswordLinkRepository _receiptApprovalRepository;
        public FunctionAppForgetPasswordLink(IForgetPasswordLinkRepository receiptApprovalRepository)
        {
            this._receiptApprovalRepository = receiptApprovalRepository;
        }

        [FunctionName("FunctionAppForgetPasswordLink")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string email = req.Query["resetPasswordEmail"];
            int result = await _receiptApprovalRepository.ForgetPasswordLinkSend(email);
            if (result == 0)
            {
                return new OkObjectResult(new
                {
                    Data = "The email address provided is not associated with any account. Please check the entered email or contact support if you need assistance.",
                    Message = "Failed",
                    IsSuccess = false
                });
            }
            else if (result == -1)
            {
                return new OkObjectResult(new
                {
                    Data = "You have exceeded the maximum number of password reset attempts. Please try again after some time or contact support if you need further assistance.",
                    Message = "Failed",
                    IsSuccess = false
                });
            }

            return new OkObjectResult(new
            {
                Data = "Your password reset request has been successfully processed. Please check your email for further instructions.",
                Message = "Success",
                IsSuccess = true
            });
        }

        [FunctionName("FunctionAppUpdateResetPassword")]
        public async Task<IActionResult> FunctionAppUpdateResetPassword(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            using StreamReader reader = new(req.Body);
            string bodyStr = await reader.ReadToEndAsync();

            var resetPassword = JsonConvert.DeserializeObject<ResetPasswordDTO>(bodyStr);
            int result = await _receiptApprovalRepository.UserResetPassword(resetPassword);
            if (result == 0)
            {
                return new OkObjectResult(new
                {
                    Data = "The password reset request could not be processed. The link may have expired or is invalid. Please request a new password reset if needed.",
                    Message = "Failed",
                    IsSuccess = false
                });
            }
            return new OkObjectResult(new
            {
                Data = "Successfully rest the password",
                Message = "Success",
                IsSuccess = true
            });
        }
    }
}
