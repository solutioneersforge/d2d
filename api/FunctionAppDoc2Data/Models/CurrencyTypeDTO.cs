using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Models;
public class CurrencyTypeDTO
{
    public int CurrencyId { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Symbol { get; set; }
}
