using System.Collections.Generic;

namespace FunctionAppDoc2Data.DataContext
{
    public partial class ExpenseSubCategory
    {
        public ExpenseSubCategory()
        {
            ReceiptItems = new HashSet<ReceiptItem>();
        }

        public int SubCategoryId { get; set; }
        public int CategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public bool? IsActive { get; set; }

        public virtual ExpenseCategory Category { get; set; }
        public virtual ICollection<ReceiptItem> ReceiptItems { get; set; }
    }
}
