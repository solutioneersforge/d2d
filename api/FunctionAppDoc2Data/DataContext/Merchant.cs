using System;
using System.Collections.Generic;

namespace FunctionAppDoc2Data.DataContext
{
    public partial class Merchant
    {
        public Merchant()
        {
            Receipts = new HashSet<Receipt>();
        }

        public int MerchantId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int CountryId { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool? IsActive { get; set; }
        public string Website { get; set; }
        public string CompanyRegNo { get; set; }
        public string TaxCompanyRegNo { get; set; }

        public virtual ICollection<Receipt> Receipts { get; set; }
    }
}
