using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Respositories;
public interface IVerificationUserRepository
{
    Task<int> UpdateVerificationUser(string authenticationKey);
}