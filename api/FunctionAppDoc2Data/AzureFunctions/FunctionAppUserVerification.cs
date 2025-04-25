using FunctionAppDoc2Data.Respositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.AzureFunctions
{
    public class FunctionAppUserVerification
    {
        private readonly IVerificationUserRepository _verificationUserRepository;

        public FunctionAppUserVerification(IVerificationUserRepository verificationUserRepository)
        {
            _verificationUserRepository = verificationUserRepository;
        }
        [FunctionName("FunctionAppUserVerification")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string verificationKey = req.Query["verificationKey"];
            int result = await _verificationUserRepository.UpdateVerificationUser(verificationKey);
            if (result == 0)
            {
                return new OkObjectResult(new
                {
                    Data = "We could not process your request. The verification link is either invalid or has expired. Please initiate a new request",
                    Message = "Failed",
                    IsSuccess = false
                });
            }
            if (result == 2)
            {
                return new OkObjectResult(new
                {
                    Data = "Your request has already been processed.",
                    Message = "Failed",
                    IsSuccess = false
                });
            }

            return new OkObjectResult(new
            {
                Data = "Verification successful. You may now proceed to log in.",
                Message = "Success",
                IsSuccess = true
            });
        }
    }
}
