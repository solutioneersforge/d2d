using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Models;
public class RejectReceiptDTO
{
    public string RejectComment { get; set; }
    public Guid ReceiptId { get; set; }
}
