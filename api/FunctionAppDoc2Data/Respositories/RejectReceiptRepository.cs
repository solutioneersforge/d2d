using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Enums;
using FunctionAppDoc2Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Respositories;
public class RejectReceiptRepository : IRejectReceiptRepository
{
    private readonly DocToDataDBContext _docToDataDBContext;
    private readonly ILogger<ExpenseTypeRepository> _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    public RejectReceiptRepository(DocToDataDBContext docToDataDBContext, ILogger<ExpenseTypeRepository> logger, IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
        _docToDataDBContext = docToDataDBContext;
        _logger = logger;
    }

    public async Task<int> RejectReceipt(RejectReceiptDTO rejectReceipt, Guid userId)
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

                var userIdsInCompany = await context.CompanyMembers
                                .Where(m => m.CompanyId == companyId)
                                .Select(m => m.UserId)
                                .ToListAsync();

                var receipts = await context.Receipts
                    .FirstOrDefaultAsync(m => m.ReceiptId == rejectReceipt.ReceiptId && userIdsInCompany.Contains(m.UserId));

                if (receipts != null)
                {
                    receipts.StatusId = (int)StatusEnum.REJECTED;
                    receipts.RejectComment = rejectReceipt.RejectComment;
                    receipts.RejectedOn = DateTime.UtcNow;
                    receipts.RejectedBy = userId;
                    await context.SaveChangesAsync();
                }

                return 0;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return 0;
        }
    }
}
