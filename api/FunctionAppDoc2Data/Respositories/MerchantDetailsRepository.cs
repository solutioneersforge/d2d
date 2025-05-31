using FunctionAppDoc2Data.Constants;
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
public class MerchantDetailsRepository : IMerchantDetailsRepository
{
    private readonly DocToDataDBContext _docToDataDBContext;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<ExpenseSubExpenseRepository> _logger;

    public MerchantDetailsRepository(DocToDataDBContext docToDataDBContext,
       IServiceScopeFactory scopeFactory, ILogger<ExpenseSubExpenseRepository> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
        _docToDataDBContext = docToDataDBContext;
    }

    public async Task<IEnumerable<MerchantDetailsDTO>> GetMerchantDetailsAsync(Guid userId)
    {
        try
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DocToDataDBContext>();
                var companyMember = await _docToDataDBContext.CompanyMembers
                             .FirstOrDefaultAsync(m => m.UserId == userId);

                List<Guid> listOfUserId;
                if (companyMember != null && companyMember.RoleId.ToString().ToLower() == RolesConstant.Manager.ToLower())
                {
                    listOfUserId = await _docToDataDBContext.CompanyMembers
                                      .Where(m => m.CompanyId == companyMember.CompanyId)
                                      .Select(m => m.UserId)
                                      .ToListAsync();
                }
                else
                {
                    listOfUserId = new List<Guid> { userId };
                }

                var merchatDetails = await _docToDataDBContext
                                .Receipts.Where(m => listOfUserId.Contains(m.UserId)).Select(m => m.Merchant).Distinct().OrderBy(m=> m.Name).ToListAsync();

                return merchatDetails.MapToMerchantDetailsDTO();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return Enumerable.Empty<MerchantDetailsDTO>();
        }
    }
}
