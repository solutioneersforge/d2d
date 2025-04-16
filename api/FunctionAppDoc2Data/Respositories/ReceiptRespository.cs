using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Mappers;
using FunctionAppDoc2Data.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Respositories;
public class ReceiptRespository : IReceiptRespository
{
    private readonly ILogger<ReceiptRespository> _logger;
    private readonly IMerchantRepository _merchantRepository;
    private readonly DocToDataDBContext _docToDataDBContext;

    public ReceiptRespository(ILogger<ReceiptRespository> logger, IMerchantRepository merchantRepository, DocToDataDBContext docToDataDBContext)
    {
        _logger = logger;
        _merchantRepository = merchantRepository;
        _docToDataDBContext = docToDataDBContext;
    }

    public async Task<int> CreateReceipt(ReceiptMasterDTO receiptMaster)
    {
        MerchantDTO merchantDTO = new MerchantDTO
        {
            Address = receiptMaster.VendorAddress,
            CountryId = 1,
            Email = receiptMaster.VendorEmail,
            IsActive = true,
            Name = receiptMaster.VendorName,
            CreatedOn = DateTime.UtcNow,
            Phone = receiptMaster.VendorPhone,
            Website = String.Empty,
            CompanyRegNo = String.Empty,
            TaxCompanyRegNo = String.Empty,
        };
        var merchantId = await _merchantRepository.CreateUpdateMerchant(merchantDTO);

        var result = receiptMaster.MapToReceiptEntity();
        result.MerchantId = merchantId;


        try
        {
            await _docToDataDBContext.AddAsync<Receipt>(result);

            var receiptImages = receiptMaster.MapToReceiptImage();
            receiptImages.ReceiptId = result.ReceiptId;
            await _docToDataDBContext.AddAsync<ReceiptImage>(receiptImages);

            await _docToDataDBContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating or updating the merchant.");
            throw new ApplicationException("An error occurred while creating or updating the merchant.", ex);
        }
        return merchantId;
    }
}
