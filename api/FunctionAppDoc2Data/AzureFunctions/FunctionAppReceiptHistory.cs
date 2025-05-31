using FunctionAppDoc2Data.Respositories;
using FunctionAppDoc2Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.AzureFunctions
{
    public class FunctionAppReceiptHistory
    {
        private readonly IReceiptHistoryRepository _receiptHistoryRepository;
        private readonly IValidatedTokenService _validatedTokenService;
        public FunctionAppReceiptHistory(IReceiptHistoryRepository receiptHistoryRepository
                    , IValidatedTokenService validatedTokenService)
        {
            _receiptHistoryRepository = receiptHistoryRepository;
            _validatedTokenService = validatedTokenService;
        }

        [FunctionName("FunctionAppReceiptHistory")]
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

                DateTime fromDate = Convert.ToDateTime(req.Query["fromDate"].ToString().ToDateOnlyOrThrow());
                DateTime toDate = Convert.ToDateTime(req.Query["toDate"].ToString().ToDateOnlyOrThrow());

                return new OkObjectResult(new
                {
                    Data = _receiptHistoryRepository.GetReceiptHistory(tokenValidation.userId, fromDate, toDate),
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

public static class DateStringMapper
    {
        public static bool TryToDateOnly(this string input, out DateTime result)
        {
            result = default;

            if (string.IsNullOrWhiteSpace(input))
                return false;

            try
            {
                // Remove time zone name in parentheses
                int parenIndex = input.IndexOf('(');
                if (parenIndex != -1)
                {
                    input = input.Substring(0, parenIndex).Trim();
                }

                // Match and fix the "GMT 0530" to "+05:30"
                var match = Regex.Match(input, @"GMT\s?([+-]?\d{4})");
                if (match.Success)
                {
                    string offset = match.Groups[1].Value;

                    // Ensure offset starts with '+' or '-'
                    if (!offset.StartsWith("+") && !offset.StartsWith("-"))
                    {
                        offset = "+" + offset;
                    }

                    // Convert "0530" -> "05:30"
                    offset = offset.Insert(3, ":");

                    // Replace the original "GMT 0530" with the correct "+05:30"
                    input = Regex.Replace(input, @"GMT\s?[+-]?\d{4}", offset);
                }

                string format = "ddd MMM dd yyyy HH:mm:ss zzz"; // No literal 'GMT' anymore
                var culture = CultureInfo.InvariantCulture;

                if (DateTimeOffset.TryParseExact(input, format, culture, DateTimeStyles.None, out var dto))
                {
                    result = dto.Date;
                    return true;
                }
            }
            catch
            {
                // Optional: logging
            }

            return false;
        }

        public static DateTime ToDateOnlyOrThrow(this string input)
        {
            if (input.TryToDateOnly(out var result))
                return result;

            throw new FormatException("Invalid date string format.");
        }
    }

}
