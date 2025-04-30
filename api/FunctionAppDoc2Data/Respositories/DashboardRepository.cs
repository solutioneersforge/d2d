using FunctionAppDoc2Data.Constants;
using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Respositories;
public class DashboardRepository : IDashboardRepository
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly DocToDataDBContext _docToDataDBContext;
    private readonly ILogger<DashboardRepository> _logger;
    public DashboardRepository(DocToDataDBContext docToDataDBContext,
        IServiceScopeFactory scopeFactory, ILogger<DashboardRepository> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
        _docToDataDBContext = docToDataDBContext;
    }

    public async Task<DashboardDTO> GetDashboardAsync(int parsedYear, int parsedMonth, Guid userId)
    {
        try
        {
            DashboardDTO dashboard = new();
            using (var scope = _scopeFactory.CreateScope())
            {
                var companyMember = _docToDataDBContext.CompanyMembers
                             .FirstOrDefault(m => m.UserId == userId);

                List<Guid> listOfUserId;
                if (companyMember != null && companyMember.RoleId.ToString().ToLower() == RolesConstant.Manager.ToLower())
                {
                    listOfUserId = _docToDataDBContext.CompanyMembers
                                      .Where(m => m.CompanyId == companyMember.CompanyId)
                                      .Select(m => m.UserId)
                                      .ToList();
                }
                else
                {
                    listOfUserId = new List<Guid> { userId };
                }
                var context = scope.ServiceProvider.GetRequiredService<DocToDataDBContext>();
                var merchantsReceipts = await context.Receipts
                    .Include(m => m.Merchant)
                     .Where(m => m.ReceiptDate.Year == parsedYear && m.ReceiptDate.Month == parsedMonth
                     && listOfUserId.Contains(m.UserId))
                    .GroupBy(m => m.MerchantId)
                    .Select(group => new MerchantDashboardDTO
                    {
                        MerchantId = group.Key.GetValueOrDefault(),
                        TotalAmount = group.Sum(m => m.TotalAmount).GetValueOrDefault(),
                        MerchantName = group.First().Merchant.Name,
                        MerchantAddress = group.First().Merchant.Address,
                    }).OrderByDescending(m => m.TotalAmount)
                    .Take(5).ToListAsync();

                merchantsReceipts = merchantsReceipts
                            .Select((item, index) => new MerchantDashboardDTO
                            {
                                Sequence = index + 1,
                                MerchantId = item.MerchantId,
                                TotalAmount = item.TotalAmount,
                                MerchantName = item.MerchantName,
                                MerchantAddress = item.MerchantAddress
                            }).ToList();



                var expenseReceipts = await context.Receipts
                    .Where(m => m.ReceiptDate.Year == parsedYear && m.ReceiptDate.Month == parsedMonth && m.UserId == userId)
                        .Include(m => m.ReceiptItems)
                            .ThenInclude(m => m.SubCategory)
                                .ThenInclude(m => m.Category)
                        .SelectMany(m => m.ReceiptItems)
                        .Where(ri => ri.SubCategoryId != null)
                        .GroupBy(ri => ri.SubCategoryId)
                        .Select(group => new ExpenseMerchantDashboardDTO
                        {
                            SubExpenseTypeId = group.Key.GetValueOrDefault(),
                            SubExpenseType = group.First().SubCategory.SubCategoryName,
                            ExpenseTypeId = group.First().SubCategory.Category.CategoryId,
                            ExpenseType = group.First().SubCategory.Category.CategoryName,
                            TotalAmount = group.Sum(ri => ri.Receipt.TotalAmount).GetValueOrDefault()
                        })
                        .ToListAsync();

                expenseReceipts = expenseReceipts.Select((item, index) => new ExpenseMerchantDashboardDTO
                {
                    SubExpenseType = item.SubExpenseType,
                    TotalAmount = item.TotalAmount,
                    ExpenseType = item.ExpenseType,
                    ExpenseTypeId = item.ExpenseTypeId,
                    Sequence = index + 1,
                    SubExpenseTypeId = item.SubExpenseTypeId
                }).ToList();

                dashboard.MerchantDashboardDTOs = merchantsReceipts;
                dashboard.ExpenseMerchantDashboardDTOs = expenseReceipts;

                var now = DateTime.Now;
                int currentYear = now.Year;
                int currentMonth = now.Month;

                var receiptsThisMonth = context.Receipts
                    .Where(r => r.ReceiptDate.Year == currentYear && r.ReceiptDate.Month == currentMonth 
                     && listOfUserId.Contains(r.UserId));

                decimal totalSpendingThisMonth = receiptsThisMonth.Sum(r => (decimal?)r.TotalAmount) ?? 0;

                int daysPassed = Math.Max(1, now.Day);

                var receiptsTillToday = context.Receipts.Where(r => r.ReceiptDate <= now && listOfUserId.Contains(r.UserId));
                dashboard.TotalSpendingTillToday = receiptsTillToday.Sum(r => (decimal?)r.TotalAmount) ?? 0;

                dashboard.AvgMonSpending = totalSpendingThisMonth;
                dashboard.AvgDailySpending = totalSpendingThisMonth / daysPassed;


                dashboard.CurrentYear = currentYear;
                return dashboard;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return new DashboardDTO();
        }
    }
}
