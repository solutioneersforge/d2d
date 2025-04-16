using System;
using System.Collections.Generic;

namespace FunctionAppDoc2Data.DataContext
{
    public partial class Item
    {
        public Item()
        {
            StockTransactions = new HashSet<StockTransaction>();
        }

        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual ICollection<StockTransaction> StockTransactions { get; set; }
    }
}
