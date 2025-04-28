using System;
using System.Collections.Generic;

namespace FunctionAppDoc2Data.DataContext
{
    public partial class PaymentType
    {
        public PaymentType()
        {
            Receipts = new HashSet<Receipt>();
        }

        public int PaymentTypeId { get; set; }
        public string PaymentType1 { get; set; }

        public virtual ICollection<Receipt> Receipts { get; set; }
    }
}
