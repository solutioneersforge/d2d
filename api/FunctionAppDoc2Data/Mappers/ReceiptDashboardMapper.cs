using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionAppDoc2Data.Mappers;
public static class ReceiptDashboardMapper
{
    public static IEnumerable<ReceiptDashboardDTO> MapToReceiptDashboard(this List<Receipt> listOfReceipt)
    {
        if (listOfReceipt == null)
            throw new ArgumentNullException(nameof(listOfReceipt));
        try
        {
            return listOfReceipt.Select(model => new ReceiptDashboardDTO
            {
                CustomerAddress = model.CustomerAddress,
                CustomerName = model.CustomerName,
                CustomerPhone = model.CustomerPhone,
                Discount = model.Discount,
                OtherCharge = model.OtherCharge,
                ReceiptDate = model.ReceiptDate,
                ReceiptId = model.ReceiptId,
                ReceiptNumber = model.ReceiptNumber,
                SubTotal = model.SubTotal.GetValueOrDefault(),
                SupplierAddress = model.Merchant.Address,
                SupplierName = model.Merchant.Name,
                SupplierPhone = model.Merchant.Phone,
                TotalAmount = model.TotalAmount.GetValueOrDefault()
            }).ToList();
        }
        catch (Exception ex)
        {
            return Enumerable.Empty<ReceiptDashboardDTO>();
        }

    }
}
