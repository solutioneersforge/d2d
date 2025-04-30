using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FunctionAppDoc2Data.Services;
public class ValidatedTokenService : IValidatedTokenService
{
    private readonly IConfiguration _config;

    public ValidatedTokenService(IConfiguration config)
    {
        _config = config;
    }

    public (bool isSuccess, Guid userId, Guid companyId) ValidateTokenRequest(HttpRequest req)
    {
        if (!req.Headers.TryGetValue("Authorization", out var authHeaderValues))
        {
            return (false, Guid.Empty, Guid.Empty);
        }

        string authorizationHeader = authHeaderValues;

        if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
        {
            return (false, Guid.Empty, Guid.Empty);
        }

        string token = authorizationHeader.Substring("Bearer ".Length).Trim();

        var validateToken = ValidateToken(token);

        if (validateToken.userId == null)
        {
            return (false, Guid.Empty,Guid.Empty);
        }
        return (true, Guid.Parse(validateToken.userId), Guid.Parse(validateToken.companyId));
    }

    private (string? userId, string? companyId) ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        string secretKey = _config["Jwt:SecretKey"];
        var key = Encoding.UTF8.GetBytes(secretKey);
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };

        SecurityToken validatedToken;
        var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

        var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier) ?? principal.FindFirst("sub");
        var companyIdClaim = principal.FindFirst("company_id");
        return (userIdClaim?.Value, companyIdClaim?.Value);
    }

    private HttpResponseData CreateResponse(HttpRequestData req, string message, System.Net.HttpStatusCode statusCode)
    {
        var response = req.CreateResponse(statusCode);
        response.Headers.Add("Content-Type", "application/json");
        response.WriteString($"{{\"message\": \"{message}\"}}");
        return response;
    }

}
