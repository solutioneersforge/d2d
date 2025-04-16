using System;
using System.Collections.Generic;

namespace FunctionAppDoc2Data.Models;
public class ReceiptVerificationMasterDTO
{
    public Guid ReceiptId { get; set; }
    public Guid UserId { get; set; }
    public string VendorName { get; set; }
    public string VendorAddress { get; set; }
    public string VendorPhone { get; set; }
    public string VendorEmail { get; set; }
    public string CustomerName { get; set; }
    public string CustomerAddress { get; set; }
    public string CustomerPhone { get; set; }
    public string InvoiceNumber { get; set; }
    public DateTime InvoiceDate { get; set; }
    public decimal SubTotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal Total { get; set; }
    public string ImagePath { get; set; }
    public string Image { get; set; }
    public string OriginalFileName { get; set; }
    public bool IsImage { get; set; }
    public List<ReceiptVerificationItemsDTO> ReceiptVerificationItems { get; set; }
}
