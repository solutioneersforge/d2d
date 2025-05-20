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
public class PaymentTypeRepository : IPaymentTypeRepository
{
    private readonly DocToDataDBContext _docToDataDBContext;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<ExpenseSubExpenseRepository> _logger;

    public PaymentTypeRepository(DocToDataDBContext docToDataDBContext,
        IServiceScopeFactory scopeFactory, ILogger<ExpenseSubExpenseRepository> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
        _docToDataDBContext = docToDataDBContext;
    }

    public async Task<IEnumerable<PaymentTypeDTO>> GetPaymentTypeAsync()
    {
        try
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DocToDataDBContext>();

                IEnumerable<PaymentType> paymentTypes = paymentTypes = await context.PaymentTypes.ToListAsync();
                return paymentTypes.MapToPaymentTypeDTO();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return Enumerable.Empty<PaymentTypeDTO>();
        }
    }
}
