using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Mappers;
using FunctionAppDoc2Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Respositories;
public class MerchantRepository : IMerchantRepository
{
    private readonly DocToDataDBContext _docToDataDBContext;
    private readonly ILogger<MerchantRepository> _logger;

    public MerchantRepository(ILogger<MerchantRepository> logger, DocToDataDBContext docToDataDBContext)
    {
        _logger = logger;
        _docToDataDBContext = docToDataDBContext;
    }

    public async Task<int> CreateUpdateMerchant(MerchantDTO merchantDTO)
    {
        if (merchantDTO == null)
        {
            throw new ArgumentNullException(nameof(merchantDTO));
        }

        try
        {
            var existingMerchantId = await GetMerchantIdIfExistsAsync(merchantDTO);
            var merchant = merchantDTO.MapToMerchantDetails(existingMerchantId.GetValueOrDefault());

            using (var transaction = await _docToDataDBContext.Database.BeginTransactionAsync())
            {
                try
                {
                    if (existingMerchantId.GetValueOrDefault() == 0)
                    {
                        await _docToDataDBContext.AddAsync(merchant).ConfigureAwait(false);
                        await _docToDataDBContext.SaveChangesAsync().ConfigureAwait(false);
                        await transaction.CommitAsync().ConfigureAwait(false);

                        _logger.LogInformation($"Merchant with ID {merchant.MerchantId} created/updated successfully.");
                        return merchant.MerchantId;
                    }
                    return existingMerchantId.GetValueOrDefault();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync().ConfigureAwait(false);
                    _logger.LogError(ex, "An error occurred while creating or updating the merchant.");
                    throw new ApplicationException("An error occurred while creating or updating the merchant.", ex);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during the merchant creation/update process.");
            throw;
        }
    }



    public async Task<int?> GetMerchantIdIfExistsAsync(MerchantDTO merchantDTO)
    {
        if (merchantDTO == null)
        {
            throw new ArgumentNullException(nameof(merchantDTO), "Merchant data cannot be null.");
        }
        _logger.LogInformation("Checking if merchant exists: {MerchantName}, {MerchantAddress}", merchantDTO.Name, merchantDTO.Address);

        try
        {
            var merchant = await _docToDataDBContext.Merchants
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Name == merchantDTO.Name && m.Address == merchantDTO.Address);

            return merchant?.MerchantId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while checking if the merchant exists.");
            throw new ApplicationException("An error occurred while fetching merchant data.", ex);
        }
    }

}
