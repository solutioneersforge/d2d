using FunctionAppDoc2Data.DataContext;
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
public class RejectReceiptRepository
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

    //public async Task<int> RejectReceipt(Guid userId, Guid receiptId, string rejectComment)
    //{
    //    try
    //    {
    //        using (var scope = _scopeFactory.CreateScope())
    //        {
    //            var context = scope.ServiceProvider.GetRequiredService<DocToDataDBContext>();
    //            var receipts = await context.Receipts.FirstOrDefaultAsync(m => m.ReceiptId == receiptId);


    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex.Message);
    //        return 0;
    //    }
    //}
}
