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
public class UnitOfMeasureRepository : IUnitOfMeasureRepository
{
    private readonly DocToDataDBContext _docToDataDBContext;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<ExpenseSubExpenseRepository> _logger;

    public UnitOfMeasureRepository(DocToDataDBContext docToDataDBContext,
        IServiceScopeFactory scopeFactory, ILogger<ExpenseSubExpenseRepository> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
        _docToDataDBContext = docToDataDBContext;
    }

    public async Task<IEnumerable<UnitOfMeasuresDTO>> GetUnitOfMeasuresAsync()
    {
        try
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DocToDataDBContext>();
                var unitOfMeasures = await context.UnitOfMeasures.ToListAsync();

                return unitOfMeasures.MapToUnitOfMeasures();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return Enumerable.Empty<UnitOfMeasuresDTO>();
        }
    }
}
