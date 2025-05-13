using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Mappers;
using FunctionAppDoc2Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Respositories;
public class ReceiptDashbboardRepository : IReceiptDashbboardRepository
{
    private readonly DocToDataDBContext _docToDataDBContext;
    private readonly IServiceScopeFactory _scopeFactory;
    public ReceiptDashbboardRepository(DocToDataDBContext docToDataDBContext, IServiceScopeFactory scopeFactory)
    {
        _docToDataDBContext = docToDataDBContext;
        _scopeFactory = scopeFactory;
    }

    public async Task<IEnumerable<ReceiptDashboardDTO>> GetReceiptDashboard(Guid userId)
    {
        try
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DocToDataDBContext>();

                var companyId = await context.CompanyMembers
                 .Where(m => m.UserId == userId)
                 .Select(m => m.CompanyId)
                 .FirstOrDefaultAsync();

                if (companyId == Guid.Empty)
                {
                    var result = context.Receipts
                        .Where(m => m.StatusId == 1 && m.UserId == userId)
                                .Include(m => m.Merchant).OrderByDescending(m => m.CreatedOn).ToList();
                    return result.MapToReceiptDashboard();
                }
                else
                {
                    var userIdsInCompany = await context.CompanyMembers
                                .Where(m => m.CompanyId == companyId)
                                .Select(m => m.UserId)
                                .ToListAsync();

                    var result = context.Receipts
                        .Where(m => m.StatusId == 1 && userIdsInCompany.Contains(m.UserId))
                                .Include(m => m.Merchant).OrderByDescending(m => m.CreatedOn).ToList();
                    return result.MapToReceiptDashboard();
                }
            }
        }
        catch (Exception ex)
        {
            return Enumerable.Empty<ReceiptDashboardDTO>();
        }
    }
}
