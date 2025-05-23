﻿using FunctionAppDoc2Data.Constants;
using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Mappers;
using FunctionAppDoc2Data.Models;
using FunctionAppDoc2Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Respositories;

public class AppRegistrationRepository : IAppRegistrationRepository
{
    private readonly ILogger<ReceiptRespository> _logger;
    private readonly IMerchantRepository _merchantRepository;
    private readonly DocToDataDBContext _docToDataDBContext;
    private readonly IAuthService _authService;

    public AppRegistrationRepository(ILogger<ReceiptRespository> logger, DocToDataDBContext docToDataDBContext, IAuthService authService)
    {
        _logger = logger;
        _docToDataDBContext = docToDataDBContext;
        _authService = authService;
    }
    public async Task<int> UpdateUserInformation(UserModelDTO userModel, Guid userId = default)
    {
        try
        {
            if (userModel == null)
            {
                throw new ArgumentNullException(nameof(userModel), "User model is null.");
            }

            await using var transaction = await _docToDataDBContext.Database.BeginTransactionAsync();

            try
            {
                var userInformation = _docToDataDBContext.Users
                    .Include(m => m.CompanyMembers)
                    .SingleOrDefault(m => m.UserId == userModel.UserId);
                if (userInformation == null)
                {
                    return 0;
                }
                userInformation.IsActive = userModel.IsActive;
                userInformation.FirstName = userModel.FirstName;
                userInformation.LastName = userModel.LastName;
                userInformation.CompanyMembers.FirstOrDefault().RoleId = userModel.RoleId;
                await _docToDataDBContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return 1;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "An error occurred, rolling back transaction.");
                throw new ApplicationException("An unexpected error occurred while creating the user.", ex);
            }
        }
        catch (DbUpdateException dbEx)
        {
            _logger.LogError(dbEx, "Database error occurred while creating the user.");
            throw new ApplicationException("A database error occurred while creating the user.", dbEx);
        }
        catch (ArgumentNullException argEx)
        {
            _logger.LogError(argEx, "Null parameter encountered.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while creating the user.");
            throw new ApplicationException("An unexpected error occurred while creating the user.", ex);
        }

    }
    public async Task<int> CreateAppRegistration(UserRegisterModelDTO userRegisterModel, Guid userId = default)
    {
        try
        {
            if (userRegisterModel == null)
            {
                throw new ArgumentNullException(nameof(userRegisterModel), "User registration model is null.");
            }

            await using var transaction = await _docToDataDBContext.Database.BeginTransactionAsync();

            try
            {
                Guid verificationKey = Guid.NewGuid();
                if (!string.IsNullOrEmpty(userRegisterModel.CompanyName))
                {
                    var existingCompany = await _docToDataDBContext.Companies
                        .FirstOrDefaultAsync(m => m.CompanyName == userRegisterModel.CompanyName);

                    if (existingCompany != null)
                    {
                        return -3;
                    }

                    var company = userRegisterModel.MapToCompany();
                    await _docToDataDBContext.Companies.AddAsync(company);
                    await _docToDataDBContext.SaveChangesAsync();

                    userRegisterModel.CompanyId = company.CompanyId;
                }

                if (userId != Guid.Empty)
                {
                    var companyMember = await _docToDataDBContext.CompanyMembers?
                            .Include(m => m.Company)?
                            .ThenInclude(m => m.Subscription)
                            .FirstOrDefaultAsync(m => m.UserId == userId);

                    if (companyMember != null)
                    {
                        if (companyMember.RoleId.ToString().ToLower() == RolesConstant.Manager.ToLower())
                        {
                            int numberOfUsers = _docToDataDBContext
                                                .CompanyMembers
                                                .Where(m => m.CompanyId == companyMember.CompanyId && m.IsActive == true)
                                                .Count();
                            int maxAccounts = companyMember.Company.Subscription.MaxAccounts;
                            if (numberOfUsers + 1 > maxAccounts)
                            {
                                return -4;
                            }
                        }
                    }

                }

                var existingUser = await _docToDataDBContext.Users
                    .SingleOrDefaultAsync(m => m.Email == userRegisterModel.Email);

                if (existingUser != null)
                {
                    return -1;
                }

                var user = userRegisterModel.MapToUserEntity();
                user.AuthenticationKey = verificationKey;
                await _docToDataDBContext.Users.AddAsync(user);
                await _docToDataDBContext.SaveChangesAsync();

                if (!string.IsNullOrEmpty(userRegisterModel.CompanyName) ||
                    (userRegisterModel.CompanyId.HasValue && userRegisterModel.CompanyId != Guid.Empty))
                {
                    var companyMember = userRegisterModel.MapToCompanyMember(user.UserId);
                    await _docToDataDBContext.CompanyMembers.AddAsync(companyMember);
                    await _docToDataDBContext.SaveChangesAsync();
                }

                await transaction.CommitAsync();
                bool isMailSentSuccess = await SendVerificationMail(userRegisterModel.Email, userRegisterModel.FirstName + ' ' + userRegisterModel.LastName, verificationKey.ToString());
                if (isMailSentSuccess)
                {
                    return 5;
                }
                return 1;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "An error occurred, rolling back transaction.");
                throw new ApplicationException("An unexpected error occurred while creating the user.", ex);
            }
        }
        catch (DbUpdateException dbEx)
        {
            _logger.LogError(dbEx, "Database error occurred while creating the user.");
            throw new ApplicationException("A database error occurred while creating the user.", dbEx);
        }
        catch (ArgumentNullException argEx)
        {
            _logger.LogError(argEx, "Null parameter encountered.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while creating the user.");
            throw new ApplicationException("An unexpected error occurred while creating the user.", ex);
        }

    }

    public async Task<(bool isSuccess, string token)> ValidateUserAsync(string usernameOrEmail, string password)
    {
        var user = await _docToDataDBContext.Users
                        .Include(m => m.CompanyMembers)
                             .ThenInclude(m => m.Role)
                        .Include(m => m.CompanyMembers)
                               .ThenInclude(m => m.Company)
            .FirstOrDefaultAsync(u => u.Email.ToLower() == usernameOrEmail.ToLower() && u.IsEmailConfirmed);

        if (user == null)
            return (false, string.Empty);

        if (VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
        {
            user.LastLoginAt = DateTime.UtcNow;
            user.FailedLoginAttempts = 0;
            try
            {
                await _docToDataDBContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error saving user login data: {ex.Message}");
            }

            return (true, _authService.GenerateJwtToken(user));
        }
        else
        {
            user.FailedLoginAttempts++;
            try
            {
                await _docToDataDBContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating failed login attempts: {ex.Message}");
            }

            return (false, string.Empty);
        }
    }

    private bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
    {
        if (storedHash == null || storedSalt == null) return false;

        using var hmac = new HMACSHA512(storedSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        if (computedHash.Length != storedHash.Length) return false;

        return CryptographicOperations.FixedTimeEquals(computedHash, storedHash);
    }


    private async Task<bool> SendVerificationMail(string toEmail, string fullName, string verificationKey)
    {
        string verificationUrl = $"https://app-doc2data.azurewebsites.net/verificationuser?verificationKey={verificationKey}";
        //string verificationUrl = $"http://localhost:4200/verificationuser?verificationKey={verificationKey}";
        string body = $@"
<html>
<head>
  <style>
    body {{
      font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
      background-color: #f4f4f7;
      color: #333333;
      margin: 0;
      padding: 0;
    }}
    .email-container {{
      max-width: 600px;
      margin: 30px auto;
      background-color: #ffffff;
      padding: 30px;
      border-radius: 8px;
      box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
    }}
    .btn {{
      display: inline-block;
      padding: 12px 24px;
      margin-top: 20px;
      font-size: 16px;
      color: #ffffff;
      background-color: #007bff;
      text-decoration: none;
      border-radius: 6px;
    }}
    .footer {{
      font-size: 12px;
      color: #888888;
      text-align: center;
      margin-top: 40px;
    }}
  </style>
</head>
<body>
  <div class='email-container'>
    <h2>Hello {fullName},</h2>
    <p>Thank you for registering with us!</p>
    <p>Please confirm your email address by clicking the button below:</p>
    <a href='{verificationUrl}' class='btn' target='_blank'>Verify Email</a>
    <p>If you did not request this, you can safely ignore this email.</p>
    <div class='footer'>
      <p>&copy; {DateTime.Now.Year} Solutioneers Forge. All rights reserved.</p>
    </div>
  </div>
</body>
</html>
";

        var mailerSendRequest = new MailerSendRequest(
                From: new("noreply@solutioneersforge.com", "Solutioneers Forge"),
                To: new MailerSendTo[] { new MailerSendTo(toEmail, fullName) },
                Subject: "Email Verification",
                Text: body,
                Html: body
            );
        return await ExternalMailService.SendMail(mailerSendRequest);
    }
}
