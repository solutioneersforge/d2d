using System;
using System.Collections.Generic;

namespace FunctionAppDoc2Data.DataContext
{
    public partial class Company
    {
        public Company()
        {
            CompanyMembers = new HashSet<CompanyMember>();
        }

        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public Guid SubscriptionId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Address { get; set; }
        public string TelephoneNumber { get; set; }

        public virtual Subscription Subscription { get; set; }
        public virtual ICollection<CompanyMember> CompanyMembers { get; set; }
    }
}
