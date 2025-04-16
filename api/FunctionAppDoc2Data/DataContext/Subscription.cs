using System;
using System.Collections.Generic;

namespace FunctionAppDoc2Data.DataContext
{
    public partial class Subscription
    {
        public Subscription()
        {
            Companies = new HashSet<Company>();
        }

        public Guid SubscriptionId { get; set; }
        public string SubscriptionName { get; set; }
        public int MaxAccounts { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
    }
}
