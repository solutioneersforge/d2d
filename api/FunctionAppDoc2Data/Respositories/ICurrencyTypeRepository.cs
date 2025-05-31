using FunctionAppDoc2Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Respositories;
public interface ICurrencyTypeRepository
{
    Task<IEnumerable<CurrencyTypeDTO>> GetCurrencyTypeAsync();
}