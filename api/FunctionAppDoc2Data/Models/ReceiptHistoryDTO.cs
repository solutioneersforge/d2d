using System;

namespace FunctionAppDoc2Data.Models;
public class ReceiptHistoryDTO
{
    public Guid ReceiptId { get; set; }
    public string ReceiptNumber { get; set; }
    public DateTime ReceiptDate { get; set; }
    public string MerchantName { get; set; }
    public string MerchantAddress { get; set; }
    public string CustomerName { get; set; }
    public string CustomerAddress { get; set; }
    public string CustomerPhone { get; set; }
    public decimal? SubTotalAmount { get; set; }
    public decimal? TotalAmount { get; set; }
    public decimal? TaxAmount { get; set; }
    public decimal? OtherCharge { get; set; }
    public int StatusId { get; set; }
    public string StatusName { get; set; }
    public decimal? ServiceChargeAmount { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string ApprovedBy { get; set; }
    public DateTime? ApprovedOn { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string Remarks { get; set; }
    public string CurrencyCode { get; set; }
}
