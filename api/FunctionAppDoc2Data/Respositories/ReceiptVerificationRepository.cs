using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Mappers;
using FunctionAppDoc2Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace FunctionAppDoc2Data.Respositories;
public class ReceiptVerificationRepository : IReceiptVerificationRepository
{
    private readonly ILogger<ReceiptVerificationRepository> _logger;
    private readonly IMerchantRepository _merchantRepository;
    private readonly DocToDataDBContext _docToDataDBContext;

    public ReceiptVerificationRepository(ILogger<ReceiptVerificationRepository> logger, IMerchantRepository merchantRepository, DocToDataDBContext docToDataDBContext)
    {
        _logger = logger;
        _merchantRepository = merchantRepository;
        _docToDataDBContext = docToDataDBContext;
    }

    public ReceiptVerificationMasterDTO GetReceiptVerification(Guid receiptId)
    {
        var result = _docToDataDBContext.Receipts
                                        .AsNoTracking()
                                        .Include(m => m.ReceiptItems)
                                        .Include(m => m.ReceiptImages)
                                        .Include(m => m.Merchant)
                                        .SingleOrDefault(m => m.ReceiptId == receiptId);

        try
        {
            return result.MapToReceiptVerificationMaster();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating or updating the merchant.");
            throw new ApplicationException("An error occurred while creating or updating the merchant.", ex);
        }
    }
}
