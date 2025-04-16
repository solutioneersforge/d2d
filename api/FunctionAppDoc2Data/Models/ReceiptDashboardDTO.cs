using System;

namespace FunctionAppDoc2Data.Models;
public class ReceiptDashboardDTO
{
    public Guid ReceiptId { get; set; }
    public string ReceiptNumber { get; set; }
    public DateTime? ReceiptDate { get; set; }
    public string SupplierName { get; set; }
    public string SupplierAddress { get; set; }
    public string SupplierPhone { get; set; }
    public string CustomerName { get; set; }
    public string CustomerPhone { get; set; }
    public string CustomerAddress { get; set; }
    public decimal SubTotal { get; set; }
    public decimal? Discount { get; set; }
    public decimal? OtherCharge { get; set; }
    public decimal TotalAmount { get; set; }

}
