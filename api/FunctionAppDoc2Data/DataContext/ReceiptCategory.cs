using System;
using System.Collections.Generic;

namespace FunctionAppDoc2Data.DataContext
{
    public partial class ReceiptCategory
    {
        public int ReceiptCategoryId { get; set; }
        public Guid ReceiptId { get; set; }
        public int CategoryId { get; set; }
        public decimal? Amount { get; set; }

        public virtual Receipt Receipt { get; set; }
    }
}
