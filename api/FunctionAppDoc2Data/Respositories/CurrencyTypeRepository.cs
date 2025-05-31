using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Mappers;
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
public class CurrencyTypeRepository : ICurrencyTypeRepository
{
    private readonly DocToDataDBContext _docToDataDBContext;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<CurrencyTypeRepository> _logger;

    public CurrencyTypeRepository(DocToDataDBContext docToDataDBContext,
       IServiceScopeFactory scopeFactory, ILogger<CurrencyTypeRepository> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
        _docToDataDBContext = docToDataDBContext;
    }


    public async Task<IEnumerable<CurrencyTypeDTO>> GetCurrencyTypeAsync()
    {
        try
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DocToDataDBContext>();

                IEnumerable<DataContext.Currency> currencyType = await context.Currencies.ToListAsync();
                return currencyType.MapToCurrencyTypeDTO();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return Enumerable.Empty<CurrencyTypeDTO>();
        }
    }
}
