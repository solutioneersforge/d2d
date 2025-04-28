using System;
using System.Collections.Generic;

namespace FunctionAppDoc2Data.DataContext
{
    public partial class Currency
    {
        public Currency()
        {
            Receipts = new HashSet<Receipt>();
        }

        public int CurrenctId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Symbol { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Receipt> Receipts { get; set; }
    }
}
