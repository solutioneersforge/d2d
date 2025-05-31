using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Enums;
using FunctionAppDoc2Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionAppDoc2Data.Mappers;
public static class ReceiptWithItemsMapper
{
    public static Receipt MapToReceiptEntity(this Models.ReceiptMasterDTO receiptMaster)
    {
        if (receiptMaster == null)
        {
            throw new ArgumentNullException(nameof(receiptMaster));
        }

        return new Receipt()
        {
            CountryId = 1,
            CreatedOn = DateTime.UtcNow,
            Discount = 0.00M,
            MerchantId = 100,
            OtherCharge = 0.00M,
            PaymentTypeId = receiptMaster.PaymentTypeId == 0 || String.IsNullOrWhiteSpace(receiptMaster.PaymentTypeId.ToString()) ? null : receiptMaster.PaymentTypeId.Value,
            ReceiptDate = ValidateDate(receiptMaster.InvoiceDate),
            ReceiptNumber = receiptMaster.InvoiceNumber,
            ServiceCharge = 0.00M,
            StatusId = (int)StatusEnum.OPEN,
            SubTotal = receiptMaster.SubTotal,
            ReceiptId = Guid.NewGuid(),
            TaxAmount = receiptMaster.TaxAmount,
            TotalAmount = receiptMaster.Total,
            UserId = receiptMaster.UserId,
            CurrencyId = receiptMaster.CurrencyId,
            CustomerAddress = receiptMaster.CustomerAddress,
            CustomerName = receiptMaster.CustomerName,
            CustomerPhone = receiptMaster.CustomerPhone,
            ReceiptItems = receiptMaster.IsStock.HasValue && receiptMaster.IsStock.Value ? GetReceiptItems(receiptMaster.ReceiptItemDTOs) : null,
            Remarks = receiptMaster.Remarks,
            SubCategoryId = receiptMaster.SubExpenseId
        };
    }

    public static ReceiptImage MapToReceiptImage(this Models.ReceiptMasterDTO receiptMaster)
    {
        if (receiptMaster == null)
        {
            throw new ArgumentNullException(nameof(receiptMaster));
        }

        return new ReceiptImage()
        {
            OriginalFileName = receiptMaster.OriginalFileName,
            ImagePath = receiptMaster.ImagePath,
            IsDelete = false,
            UploadedDateTime = DateTime.UtcNow
        };

    }

    private static DateTime ValidateDate(DateTime validDate)
    {
        if (validDate < new DateTime(1753, 1, 1) || validDate > new DateTime(9999, 12, 31))
        {
            return DateTime.UtcNow;
        }
        return validDate;
    }

    private static ICollection<DataContext.ReceiptItem> GetReceiptItems(List<ReceiptItemDTO> receiptItems)
    {
        if (receiptItems == null) return null;
        return receiptItems.Select(m => new DataContext.ReceiptItem()
        {
            ItemDescription = m.ItemDescription,
            Discount = m.Discount,
            Quantity = m.Quantity,
            SubTotal = m.Total,
            UnitPrice = m.UnitPrice,
            SubCategoryId = m.SubCategoryId == 0 ? null : m.SubCategoryId,
            ReceiptItemId = Guid.NewGuid(),
        }).ToList();
    }
}
