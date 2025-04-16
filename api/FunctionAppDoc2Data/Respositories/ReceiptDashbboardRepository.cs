using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Mappers;
using FunctionAppDoc2Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Respositories;
public class ReceiptDashbboardRepository : IReceiptDashbboardRepository
{
    private readonly DocToDataDBContext _docToDataDBContext;

    public ReceiptDashbboardRepository(DocToDataDBContext docToDataDBContext)
    {
        _docToDataDBContext = docToDataDBContext;
    }

    public async Task<IEnumerable<ReceiptDashboardDTO>> GetReceiptDashboard(Guid userId)
    {
        try
        {
            var result = _docToDataDBContext.Receipts
                    .Where(m => m.StatusId == 1 && m.UserId == userId)
                            .Include(m => m.Merchant).OrderByDescending(m => m.CreatedOn).ToList();
            return result.MapToReceiptDashboard();
        }
        catch (Exception ex)
        {
            return Enumerable.Empty<ReceiptDashboardDTO>();
        }
    }
}
