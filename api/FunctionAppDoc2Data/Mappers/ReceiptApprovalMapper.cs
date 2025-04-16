using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Enums;
using FunctionAppDoc2Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionAppDoc2Data.Mappers;
public static class ReceiptApprovalMapper
{
    public static Receipt MapToReceipt(this ReceiptApprovalDTO receiptApprovalDTO)
    {
        if (receiptApprovalDTO == null)
            throw new ArgumentNullException(nameof(ReceiptApprovalDTO));

        return new Receipt()
        {
            CountryId = 1,
            CustomerAddress = receiptApprovalDTO.CustomerAddress,
            CustomerPhone = receiptApprovalDTO.CustomerPhone,
            CustomerName = receiptApprovalDTO.CustomerName,
            MerchantId = receiptApprovalDTO.MerchantId,
            OtherCharge = receiptApprovalDTO.OtherCharge,
            PaymentTypeId = 1,
            ReceiptDate = receiptApprovalDTO.ReceiptDate,
            ReceiptId = receiptApprovalDTO.ReceiptId,
            ServiceCharge = receiptApprovalDTO.ServiceCharge,
            ReceiptNumber = receiptApprovalDTO.ReceiptNumber,
            StatusId = receiptApprovalDTO.StatusId,
            SubTotal = receiptApprovalDTO.SubTotal,
            TaxAmount = receiptApprovalDTO.TaxAmount,
            TotalAmount = receiptApprovalDTO.TotalAmount,
            ApprovedBy = receiptApprovalDTO.ApprovedBy,
            ApprovedOn = receiptApprovalDTO.ApprovedOn,
            UserId = receiptApprovalDTO.UserId,
            CreatedOn = DateTime.UtcNow,
            CurrencyId = 1,
            ReceiptItems = GetReceiptItems(receiptApprovalDTO.ReceiptItemsApproval.Where(m => !String.IsNullOrEmpty(m.ItemDescription)).ToList())
        };
    }

    private static ICollection<DataContext.ReceiptItem> GetReceiptItems(List<ReceiptItemsApprovalDTO> receiptItems)
    {
        return receiptItems?.Select(m => new DataContext.ReceiptItem
        {
            ItemDescription = m.ItemDescription ?? string.Empty,  // Prevent potential null issues
            Discount = m.Discount,
            Quantity = m.Quantity,
            SubTotal = m.Total,
            UnitPrice = m.UnitPrice,
            SubCategoryId = m.SubCategoryId,
            UnitOfMeasureId = m.UnitOfMeasureId
        }).ToList() ?? new List<DataContext.ReceiptItem>();
    }

    public static Receipt MapToReceiptForUpdate(this Receipt receipt, ReceiptApprovalDTO receiptApprovalDTO)
    {
        if (receiptApprovalDTO == null)
            throw new ArgumentNullException(nameof(ReceiptApprovalDTO));

        receipt.CountryId = 1;
        receipt.CustomerAddress = receiptApprovalDTO.CustomerAddress;
        receipt.CustomerPhone = receiptApprovalDTO.CustomerPhone;
        receipt.CustomerName = receiptApprovalDTO.CustomerName;
        receipt.MerchantId = receiptApprovalDTO.MerchantId;
        receipt.OtherCharge = receiptApprovalDTO.OtherCharge;
        receipt.PaymentTypeId = 1;
        receipt.ReceiptDate = receiptApprovalDTO.ReceiptDate;
        receipt.ReceiptId = receiptApprovalDTO.ReceiptId;
        receipt.ServiceCharge = receiptApprovalDTO.ServiceCharge;
        receipt.ReceiptNumber = receiptApprovalDTO.ReceiptNumber;
        receipt.StatusId = receiptApprovalDTO.StatusId;
        receipt.SubTotal = receiptApprovalDTO.SubTotal;
        receipt.TaxAmount = receiptApprovalDTO.TaxAmount;
        receipt.TotalAmount = receiptApprovalDTO.TotalAmount;
        if (receiptApprovalDTO.StatusId == (int)StatusEnum.APPROVED)
        {
            receipt.ApprovedBy = receiptApprovalDTO.ApprovedBy;
            receipt.ApprovedOn = receiptApprovalDTO.ApprovedOn;
        }
        else
        {
            receipt.UpdatedOn = receiptApprovalDTO.ApprovedOn;
            receipt.ModifiedBy = receiptApprovalDTO.ApprovedBy;
        }

        receipt.ReceiptItems = GetReceiptItems(receiptApprovalDTO.ReceiptItemsApproval.Where(m => !string.IsNullOrEmpty(m.ItemDescription)).ToList());

        return receipt;
    }
}
