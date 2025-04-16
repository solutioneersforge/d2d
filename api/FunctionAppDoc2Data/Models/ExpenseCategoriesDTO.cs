using System.Collections.Generic;

namespace FunctionAppDoc2Data.Models;
public class ExpenseCategoriesDTO
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public bool IsActive { get; set; }
    public IEnumerable<ExpenseSubCategoriesDTO> ExpenseSubCategoriesDTOs { get; set; }
}
