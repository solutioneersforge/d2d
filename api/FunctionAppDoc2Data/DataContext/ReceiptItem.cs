using System;
using System.Collections.Generic;

namespace FunctionAppDoc2Data.DataContext
{
    public partial class ReceiptItem
    {
        public Guid ReceiptItemId { get; set; }
        public Guid ReceiptId { get; set; }
        public string ItemDescription { get; set; }
        public int? UnitOfMeasureId { get; set; }
        public int? SubCategoryId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal SubTotal { get; set; }

        public virtual Receipt Receipt { get; set; }
        public virtual ExpenseSubCategory SubCategory { get; set; }
        public virtual UnitOfMeasure UnitOfMeasure { get; set; }
    }
}
