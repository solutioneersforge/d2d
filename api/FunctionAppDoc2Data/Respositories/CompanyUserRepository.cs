using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Mappers;
using FunctionAppDoc2Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace FunctionAppDoc2Data.Respositories;
public class CompanyUserRepository : ICompanyUserRepository
{
    private readonly DocToDataDBContext _docToDataDBContext;
    private readonly ILogger<CompanyUserRepository> _logger;
    public CompanyUserRepository(DocToDataDBContext docToDataDBContext, ILogger<CompanyUserRepository> logger)
    {
        _docToDataDBContext = docToDataDBContext;
        _logger = logger;
    }

    public async Task<IEnumerable<CompanyUserDTO>> GetAllCompanyUsers(Guid userId)
    {
        if (userId == Guid.Empty)
        {
            return Enumerable.Empty<CompanyUserDTO>();
        }
        var companyId = _docToDataDBContext.CompanyMembers.FirstOrDefault(m => m.UserId == userId)?.CompanyId;
        if (companyId == null)
        {
            return Enumerable.Empty<CompanyUserDTO>();
        }
        var companyUserResult = await _docToDataDBContext.CompanyMembers
                    .Where(m => m.CompanyId == companyId)
                    .Include(m => m.User)
                    .Include(m => m.Role)
                    .ToListAsync();

        return companyUserResult.GetCompanyUserDTOs();
    }

    public async Task<int> UpdateCompanyDetails(Guid companyId, CompanyUpdateDTO companyUpdate)
    {
        if (companyId == Guid.Empty || companyUpdate == null)
        {
            return 0;
        }

        var companyResult = await _docToDataDBContext.Companies
            .FirstOrDefaultAsync(m => m.CompanyId == companyId);

        if (companyResult == null)
        {
            return 0;
        }

        companyResult.CompanyEmail = companyUpdate.CompanyEmail;
        companyResult.CompanyName = companyUpdate.CompanyName;
        companyResult.TelephoneNumber = companyUpdate.CompanyPhoneNumber;
        companyResult.Address = companyUpdate.CompanyAddress;

        return await _docToDataDBContext.SaveChangesAsync();
    }
}
