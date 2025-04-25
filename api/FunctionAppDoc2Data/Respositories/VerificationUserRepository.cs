using FunctionAppDoc2Data.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Respositories;
public class VerificationUserRepository : IVerificationUserRepository
{
    private readonly DocToDataDBContext _docToDataDBContext;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<ExpenseSubExpenseRepository> _logger;

    public VerificationUserRepository(DocToDataDBContext docToDataDBContext,
        IServiceScopeFactory scopeFactory, ILogger<ExpenseSubExpenseRepository> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
        _docToDataDBContext = docToDataDBContext;
    }

    public async Task<int> UpdateVerificationUser(string authenticationKey)
    {
        try
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DocToDataDBContext>();
                var authenticationUser = await context.Users
                                .SingleOrDefaultAsync(m => m.AuthenticationKey == Guid.Parse(authenticationKey));
                if (authenticationUser == null)
                {
                    return 0;
                }
                else if (authenticationUser.IsEmailConfirmed)
                {
                    return 2;
                }
                authenticationUser.IsEmailConfirmed = true;
                await context.SaveChangesAsync();
                return 1;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return 0;
        }
    }
}
