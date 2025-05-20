using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Models;
public class MerchantDetailsDTO
{
    public int MerchantId { get; set; }
    public string MerchantName { get; set; }
    public string MerchantAddress { get; set; }
    public string MerchantPhone { get; set; }
    public string MerchantEmail { get; set; }
}
