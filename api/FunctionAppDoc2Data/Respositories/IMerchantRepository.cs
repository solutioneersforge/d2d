using FunctionAppDoc2Data.Models;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Respositories;
public interface IMerchantRepository
{
    Task<int> CreateUpdateMerchant(MerchantDTO merchantDTO);
    Task<int?> GetMerchantIdIfExistsAsync(MerchantDTO merchantDTO);
}