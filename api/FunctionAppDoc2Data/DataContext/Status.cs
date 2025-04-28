using System;
using System.Collections.Generic;

namespace FunctionAppDoc2Data.DataContext
{
    public partial class Status
    {
        public Status()
        {
            Receipts = new HashSet<Receipt>();
        }

        public int StatusId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Receipt> Receipts { get; set; }
    }
}
