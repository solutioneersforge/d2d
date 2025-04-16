using System;
using System.Collections.Generic;

namespace FunctionAppDoc2Data.DataContext
{
    public partial class Receipt
    {
        public Receipt()
        {
            ReceiptCategories = new HashSet<ReceiptCategory>();
            ReceiptImages = new HashSet<ReceiptImage>();
            ReceiptItems = new HashSet<ReceiptItem>();
        }

        public Guid ReceiptId { get; set; }
        public Guid UserId { get; set; }
        public string ReceiptNumber { get; set; }
        public DateTime ReceiptDate { get; set; }
        public int? MerchantId { get; set; }
        public int CountryId { get; set; }
        public int PaymentTypeId { get; set; }
        public int CurrencyId { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? Discount { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? ServiceCharge { get; set; }
        public decimal? OtherCharge { get; set; }
        public decimal? TaxAmount { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int StatusId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public Guid? ModifiedBy { get; set; }
        public Guid? ApprovedBy { get; set; }
        public DateTime? ApprovedOn { get; set; }

        public virtual Country Country { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual Merchant Merchant { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public virtual Status Status { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ReceiptCategory> ReceiptCategories { get; set; }
        public virtual ICollection<ReceiptImage> ReceiptImages { get; set; }
        public virtual ICollection<ReceiptItem> ReceiptItems { get; set; }
    }
}
