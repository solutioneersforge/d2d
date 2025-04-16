using FunctionAppDoc2Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Respositories;
public interface IUnitOfMeasureRepository
{
    Task<IEnumerable<UnitOfMeasuresDTO>> GetUnitOfMeasuresAsync();
}
