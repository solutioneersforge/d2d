using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Models;
using System;
using System.Security.Cryptography;
using System.Text;

namespace FunctionAppDoc2Data.Mappers;
public static class UserMapper
{
    public static User MapToUserEntity(this UserRegisterModelDTO userRegisterModel)
    {
        try
        {
            if (userRegisterModel == null)
                throw new ArgumentNullException(nameof(userRegisterModel));

            var hasPasword = HashPassword(userRegisterModel.Password);
            return new User()
            {
                CreatedAt = DateTime.UtcNow,
                Email = userRegisterModel.Email,
                FailedLoginAttempts = 0,
                FirstName = userRegisterModel.FirstName,
                LastName = userRegisterModel.LastName,
                IsEmailConfirmed = false,
                IsTwoFactorEnabled = false,
                PasswordHash = hasPasword.hash,
                PasswordSalt = hasPasword.salt
            };
        }
        catch (Exception ex)
        {
            return new User();
        }
    }

    public static (byte[] hash, byte[] salt) HashPassword(string password)
    {
        using var hmac = new HMACSHA512();
        return (hmac.ComputeHash(Encoding.UTF8.GetBytes(password)), hmac.Key);
    }
}
