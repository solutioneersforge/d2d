using System;
using System.Collections.Generic;

namespace FunctionAppDoc2Data.DataContext
{
    public partial class Currency
    {
        public Currency()
        {
            Receipts = new HashSet<Receipt>();
            Users = new HashSet<User>();
        }

        public int CurrenctId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Symbol { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Receipt> Receipts { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
