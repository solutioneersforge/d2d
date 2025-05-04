using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Mappers;
using FunctionAppDoc2Data.Models;
using FunctionAppDoc2Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Respositories;
public class ForgetPasswordLinkRepository : IForgetPasswordLinkRepository
{
    private readonly DocToDataDBContext _docToDataDBContext;
    private readonly ILogger<CompanyUserRepository> _logger;
    public ForgetPasswordLinkRepository(DocToDataDBContext docToDataDBContext, ILogger<CompanyUserRepository> logger)
    {
        _docToDataDBContext = docToDataDBContext;
        _logger = logger;
    }

    public async Task<int> ForgetPasswordLinkSend(string emailAddress)
    {
        if (String.IsNullOrEmpty(emailAddress))
        {
            return 0;
        }

        var userObject = await _docToDataDBContext.Users.SingleOrDefaultAsync(m => m.Email == emailAddress);
        if (userObject == null)
        {
            return 0;
        }

        int forgetPasswordRetry = (userObject.ForgetPasswordRetry ?? 0) + 1;

        string envForgetPassword = Environment.GetEnvironmentVariable("ForgetPasswordRetry") ?? "3";

        if(Convert.ToInt32(envForgetPassword) < forgetPasswordRetry)
        {
            return -1;
        }
        int ForgetPasswordExpiredHours = String.IsNullOrEmpty(Environment.GetEnvironmentVariable("ForgetPasswordExpiredHours").ToString()) ?
           24 :  Convert.ToInt32(Environment.GetEnvironmentVariable("ForgetPasswordExpiredHours"));
        var forgetPasswordKey = Guid.NewGuid();
        userObject.ForgetPasswordKey = forgetPasswordKey;
        userObject.ForgetPasswordRetry = forgetPasswordRetry;
        userObject.ExpiredForgetPasswordKey = DateTime.UtcNow.AddHours(ForgetPasswordExpiredHours);
        userObject.IsExpiredKeyUsed = false;
        await _docToDataDBContext.SaveChangesAsync();


        var forgetPasswordMailLink = new ForgetPasswordMailLink();
        bool isSuccess = await forgetPasswordMailLink.SendForgetPasswordLink(emailAddress, forgetPasswordKey);

        return 1;
    }


    public async Task<int> UserResetPassword(ResetPasswordDTO resetPassword)
    {
        if(resetPassword == null)
        {
            return 0;
        }
        else if(string.IsNullOrEmpty(resetPassword.ResetPassword))
        {
            return 0;
        }

        var userObject = await _docToDataDBContext.Users
            .SingleOrDefaultAsync(m => m.ForgetPasswordKey == resetPassword.ForgetPasswordToken);

        if (userObject == null)
        {
            return 0;
        }

        if (userObject.IsExpiredKeyUsed.GetValueOrDefault() == true && userObject.ExpiredForgetPasswordKey < DateTime.UtcNow)
        {
            return 0;
        }

        var userHashCode = UserMapper.HashPassword(resetPassword.ResetPassword);
        userObject.PasswordHash = userHashCode.hash;
        userObject.PasswordSalt = userHashCode.salt;
        userObject.ForgetPasswordRetry = 0;
        userObject.IsExpiredKeyUsed = true;
        await _docToDataDBContext.SaveChangesAsync();
        return 1;
    }


}
