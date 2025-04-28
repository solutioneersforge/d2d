using System;
using System.Collections.Generic;

namespace FunctionAppDoc2Data.DataContext
{
    public partial class TransactionType
    {
        public TransactionType()
        {
            StockTransactions = new HashSet<StockTransaction>();
        }

        public int TransactionTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<StockTransaction> StockTransactions { get; set; }
    }
}
