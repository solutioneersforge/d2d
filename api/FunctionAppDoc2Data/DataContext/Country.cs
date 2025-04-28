using System;
using System.Collections.Generic;

namespace FunctionAppDoc2Data.DataContext
{
    public partial class Country
    {
        public Country()
        {
            Receipts = new HashSet<Receipt>();
        }

        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public virtual ICollection<Receipt> Receipts { get; set; }
    }
}
