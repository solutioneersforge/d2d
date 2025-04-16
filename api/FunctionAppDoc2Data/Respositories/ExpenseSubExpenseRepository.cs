
using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Mappers;
using FunctionAppDoc2Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Respositories;
public class ExpenseSubExpenseRepository : IExpenseSubExpenseRepository
{
    private readonly DocToDataDBContext _docToDataDBContext;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<ExpenseSubExpenseRepository> _logger;

    public ExpenseSubExpenseRepository(DocToDataDBContext docToDataDBContext,
        IServiceScopeFactory scopeFactory, ILogger<ExpenseSubExpenseRepository> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
        _docToDataDBContext = docToDataDBContext;
    }

    public async Task<IEnumerable<ExpenseCategoriesDTO>> GetExpenseSubExpenseCategoryAsync(Guid userId)
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

                IEnumerable<ExpenseCategory> expenseCategories = null;
                if (companyId == Guid.Empty)
                {
                    expenseCategories = await context.ExpenseCategories
                                        .Where(m => m.UserId == userId)
                                        .Include(m => m.ExpenseSubCategories)
                                        .ToListAsync();
                }
                else
                {
                    var userIdsInCompany = await context.CompanyMembers
                            .Where(m => m.CompanyId == companyId)
                            .Select(m => m.UserId)
                            .ToListAsync();

                    expenseCategories = await context.ExpenseCategories
                                            .Where(m => m.UserId.HasValue && userIdsInCompany.Contains(m.UserId.Value))
                                            .Include(m => m.ExpenseSubCategories)
                                            .ToListAsync();
                }
                return expenseCategories.MapToExpenseCategoriesDTO();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return Enumerable.Empty<ExpenseCategoriesDTO>();
        }
    }
}

