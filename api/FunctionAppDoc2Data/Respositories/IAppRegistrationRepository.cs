using FunctionAppDoc2Data.Models;
using System;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Respositories;
public interface IAppRegistrationRepository
{
    Task<int> CreateAppRegistration(UserRegisterModelDTO userRegisterModel, Guid userId = default);
    Task<(bool isSuccess, string token)> ValidateUserAsync(string usernameOrEmail, string password);
    Task<int> UpdateUserInformation(UserModelDTO userModel, Guid userId = default);
}