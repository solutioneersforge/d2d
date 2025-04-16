using FunctionAppDoc2Data.Respositories;
using FunctionAppDoc2Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.AzureFunctions
{
    public class FunctionAppDashboardReport
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IConfiguration _config;
        private readonly IValidatedTokenService _validatedTokenService;

        public FunctionAppDashboardReport(IDashboardRepository dashboardRepository,
            IConfiguration config, IValidatedTokenService validatedTokenService)
        {
            _dashboardRepository = dashboardRepository;
            _config = config;
            _validatedTokenService = validatedTokenService;
        }

        [FunctionName("FunctionAppDashboardReport")]
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

                string year = req.Query["year"];
                string month = req.Query["month"];

                int.TryParse(year, out int parsedYear);
                int.TryParse(month, out int parsedMonth);

                var resultData = await _dashboardRepository.GetDashboardAsync(parsedYear, parsedMonth, tokenValidation.userId);
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
