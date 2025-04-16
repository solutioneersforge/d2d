using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Mappers;
using FunctionAppDoc2Data.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Respositories;
public class ReceiptApprovalRepository : IReceiptApprovalRepository
{
    private readonly DocToDataDBContext _docToDataDBContext;
    private readonly ILogger<ReceiptApprovalRepository> _logger;
    private readonly IMerchantRepository _merchantRepository;
    public ReceiptApprovalRepository(ILogger<ReceiptApprovalRepository> logger, DocToDataDBContext docToDataDBContext, IMerchantRepository merchantRepository)
    {
        _logger = logger;
        _docToDataDBContext = docToDataDBContext;
        _merchantRepository = merchantRepository;
    }

    public async Task CreateUpdateReceiptAndItems(ReceiptApprovalDTO receiptApproval)
    {
        MerchantDTO merchantDTO = new MerchantDTO
        {
            Address = receiptApproval.MerchantAddress,
            CountryId = 1,
            Email = receiptApproval.MerchantEmail,
            IsActive = true,
            Name = receiptApproval.MerchantName,
            CreatedOn = DateTime.UtcNow,
            Phone = receiptApproval.MerchantPhone,
            Website = String.Empty,
            CompanyRegNo = String.Empty,
            TaxCompanyRegNo = String.Empty,
        };
        var merchantId = await _merchantRepository.CreateUpdateMerchant(merchantDTO);
        receiptApproval.MerchantId = merchantId;
        Receipt receipt = _docToDataDBContext.Receipts.SingleOrDefault(m => m.ReceiptId == receiptApproval.ReceiptId);
        var receiptItem = _docToDataDBContext.ReceiptItems.Where(m => m.ReceiptId == receiptApproval.ReceiptId);
        if (receipt != null)
        {
            _docToDataDBContext.ReceiptItems.RemoveRange(receiptItem);
            var resultMapper = receipt.MapToReceiptForUpdate(receiptApproval);
            await _docToDataDBContext.SaveChangesAsync();
        }
        else
        {
            var resultMapper = receiptApproval.MapToReceipt();
            await _docToDataDBContext.AddAsync<Receipt>(resultMapper);
            await _docToDataDBContext.SaveChangesAsync();
        }
    }
}
