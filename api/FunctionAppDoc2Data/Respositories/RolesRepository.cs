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
public class RolesRepository : IRolesRepository
{
    private readonly DocToDataDBContext _docToDataDBContext;
    private readonly ILogger<ExpenseTypeRepository> _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    public RolesRepository(DocToDataDBContext docToDataDBContext, ILogger<ExpenseTypeRepository> logger, IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
        _docToDataDBContext = docToDataDBContext;
        _logger = logger;
    }

    public async Task<IEnumerable<RolesDTO>> GetActiveRoles()
    {
        try
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DocToDataDBContext>();
                var roles = await context.Roles.ToListAsync();

                return roles.MapToRolesDTO();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return Enumerable.Empty<RolesDTO>();
        }
    }
}
