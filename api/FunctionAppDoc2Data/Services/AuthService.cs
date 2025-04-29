using FunctionAppDoc2Data.DataContext;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace FunctionAppDoc2Data.Services;
public class AuthService : IAuthService
{
    private readonly IConfiguration _config;

    public AuthService(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateJwtToken(User user)
    {
        var secretKey = _config["Jwt:SecretKey"];
        var issuer = _config["Jwt:Issuer"];
        var audience = _config["Jwt:Audience"];

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName,$"{user.FirstName} {user.LastName}"),
                new Claim("role_name", user.CompanyMembers.Count == 0  ? "Individual" : user.CompanyMembers.FirstOrDefault(m => m.UserId == user.UserId).Role.RoleName),
                new Claim("company_id", user.CompanyMembers.Count == 0  ? "" : user.CompanyMembers.FirstOrDefault(m => m.UserId == user.UserId)?.CompanyId.ToString()),
                new Claim("company_name", user.CompanyMembers.Count == 0  ? "" : user.CompanyMembers.FirstOrDefault(m => m.UserId == user.UserId)?.Company?.CompanyName ?? ""),
                new Claim("company_email", user.CompanyMembers.Count == 0  ? "" : user.CompanyMembers.FirstOrDefault(m => m.UserId == user.UserId)?.Company?.CompanyEmail ?? ""),
                new Claim("company_address", user.CompanyMembers.Count == 0  ? "" : user.CompanyMembers.FirstOrDefault(m => m.UserId == user.UserId)?.Company?.Address ?? ""),
                new Claim("company_phone", user.CompanyMembers.Count == 0  ? "" : user.CompanyMembers.FirstOrDefault(m => m.UserId == user.UserId)?.Company?.TelephoneNumber ?? ""),
        };

        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
