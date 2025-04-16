using FunctionAppDoc2Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Respositories;
public interface IReceiptDashbboardRepository
{
    Task<IEnumerable<ReceiptDashboardDTO>> GetReceiptDashboard(Guid userId);
}