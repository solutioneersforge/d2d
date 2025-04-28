using System;
using System.Collections.Generic;

namespace FunctionAppDoc2Data.DataContext
{
    public partial class UnitOfMeasure
    {
        public UnitOfMeasure()
        {
            ReceiptItems = new HashSet<ReceiptItem>();
        }

        public int UnitOfMeasureId { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<ReceiptItem> ReceiptItems { get; set; }
    }
}
