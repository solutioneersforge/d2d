using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Models;
using FunctionAppDoc2Data.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FunctionAppDoc2Data.Mappers;
public static class ReceiptVerificationMapper
{
    public static ReceiptVerificationMasterDTO MapToReceiptVerificationMaster(this Receipt receipt)
    {
        if (receipt == null)
        {
            throw new ArgumentNullException(nameof(receipt));
        }

        return new ReceiptVerificationMasterDTO()
        {
            ReceiptId = receipt.ReceiptId,
            CustomerAddress = receipt?.CustomerAddress ?? String.Empty,
            CustomerName = receipt?.CustomerName ?? String.Empty,
            CustomerPhone = receipt?.CustomerPhone ?? String.Empty,
            InvoiceDate = receipt.ReceiptDate,
            SubTotal = receipt.SubTotal.GetValueOrDefault(),
            TaxAmount = receipt.TaxAmount.GetValueOrDefault(),
            Total = receipt.TotalAmount.GetValueOrDefault(),
            InvoiceNumber = receipt.ReceiptNumber,
            UserId = receipt.ReceiptId,
            VendorAddress = receipt.Merchant?.Address ?? String.Empty,
            VendorEmail = receipt.Merchant?.Email ?? String.Empty,
            VendorName = receipt.Merchant?.Name ?? String.Empty,
            VendorPhone = receipt.Merchant?.Phone ?? String.Empty,
            Image = UploadImageToAzure.DownloadImage(receipt.ReceiptImages?.FirstOrDefault()?.ImagePath),
            ImagePath = receipt.ReceiptImages?.FirstOrDefault()?.ImagePath,
            IsImage = ValidateImageType(receipt.ReceiptImages?.FirstOrDefault()?.ImagePath),
            ReceiptVerificationItems = receipt.ReceiptItems?.Select(item => new ReceiptVerificationItemsDTO()
            {
                Discount = item.Discount,
                ItemDescription = item.ItemDescription,
                Quantity = item.Quantity,
                ReceiptItemID = item.ReceiptItemId,
                SubCategoryId = item.SubCategoryId.GetValueOrDefault(),
                Total = item.SubTotal,
                UnitPrice = item.UnitPrice,
                UnitOfMeasureId = item.UnitOfMeasureId.GetValueOrDefault()
            }).ToList() ?? new List<ReceiptVerificationItemsDTO>()
        };
    }

    private static bool ValidateImageType(string imagePath)
    {
        if (String.IsNullOrWhiteSpace(imagePath)) return true;
        string fileExtension = Path.GetExtension(imagePath).ToLower();
        if (fileExtension == ".pdf")
        {
            return false;
        }
        else if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".gif")
        {
            return true;
        }
        else
        {
            return true;
        }
    }
}
