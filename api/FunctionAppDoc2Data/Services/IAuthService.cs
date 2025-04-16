using FunctionAppDoc2Data.DataContext;

namespace FunctionAppDoc2Data.Services;
public interface IAuthService
{
    string GenerateJwtToken(User user);
}