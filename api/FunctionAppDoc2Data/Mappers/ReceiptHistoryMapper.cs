using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionAppDoc2Data.Mappers;
public static class ReceiptHistoryMapper
{
    public static IEnumerable<ReceiptHistoryDTO> MapToReceiptHistory(this List<Receipt> listOfReceipt)
    {
        if (listOfReceipt == null)
            throw new ArgumentNullException(nameof(listOfReceipt));

        return listOfReceipt.Select(m => new ReceiptHistoryDTO()
        {
            CustomerAddress = m.CustomerAddress,
            CustomerName = m.CustomerName,
            CustomerPhone = m.CustomerPhone,
            MerchantAddress = m.Merchant.Address,
            OtherCharge = m.OtherCharge,
            ReceiptDate = m.ReceiptDate,
            ReceiptId = m.ReceiptId,
            ReceiptNumber = m.ReceiptNumber,
            ServiceChargeAmount = m.ServiceCharge,
            StatusId = m.StatusId,
            StatusName = m.Status.Name,
            SubTotalAmount = m.SubTotal,
            MerchantName = m.Merchant.Name,
            TaxAmount = m.TaxAmount,
            TotalAmount = m.TotalAmount,
            CreatedBy = m.User.FirstName,
            ApprovedBy = m.ApprovedByNavigation != null ? $"{m.ApprovedByNavigation.FirstName} {m.ApprovedByNavigation.LastName}" : "",
            ApprovedOn = m.ApprovedOn,
            ModifiedBy = m.ModifiedByNavigation != null ? $"{m.ModifiedByNavigation.FirstName} {m.ModifiedByNavigation.LastName}" : "",
            ModifiedOn = m.UpdatedOn,
            CreatedOn = m.CreatedOn,
            Remarks = m.Remarks,
            CurrencyCode = m.Currency.Name
        });
    }
}
