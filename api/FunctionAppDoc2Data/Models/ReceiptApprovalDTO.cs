using System;
using System.Collections.Generic;

namespace FunctionAppDoc2Data.Models;
public class ReceiptApprovalDTO
{
    public Guid ReceiptId { get; set; }
    public Guid ApprovedBy { get; set; }
    public Guid UserId { get; set; }
    public DateTime ApprovedOn { get; } = DateTime.UtcNow;
    public int MerchantId { get; set; }
    public string MerchantName { get; set; }
    public string MerchantAddress { get; set; }
    public string MerchantPhone { get; set; }
    public string MerchantEmail { get; set; }
    public string CustomerName { get; set; }
    public string CustomerAddress { get; set; }
    public string CustomerPhone { get; set; }
    public string ReceiptNumber { get; set; }
    public DateTime ReceiptDate { get; set; }
    public decimal SubTotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal ServiceCharge { get; set; } = 0;
    public decimal OtherCharge { get; set; } = 0;
    public int StatusId { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal Discount { get; set; } = 0;
    public int? PaymentTypeId { get; set; }
    public List<ReceiptItemsApprovalDTO> ReceiptItemsApproval { get; set; }

}
