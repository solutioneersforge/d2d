using FunctionAppDoc2Data.Models;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Respositories;
public interface IForgetPasswordLinkRepository
{
    Task<int> ForgetPasswordLinkSend(string emailAddress);
    Task<int> UserResetPassword(ResetPasswordDTO resetPassword);
}