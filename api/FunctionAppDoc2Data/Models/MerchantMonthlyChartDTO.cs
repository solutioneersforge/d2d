using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Models;
public class MerchantChartDTO
{
    public string MerchantName { get; set; }
    public decimal Total { get; set; }
}

public class MerchantMonthlyChartDTO
{
    public string MonthName { get; set; }
    public decimal Total { get; set; }
}
