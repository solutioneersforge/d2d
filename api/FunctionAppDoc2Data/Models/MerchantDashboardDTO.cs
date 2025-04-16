namespace FunctionAppDoc2Data.Models;
public class MerchantDashboardDTO
{
    public int Sequence { get; set; }
    public int MerchantId { get; set; }
    public string MerchantName { get; set; }
    public string MerchantAddress { get; set; }
    public decimal TotalAmount { get; set; }
}


public class ExpenseMerchantDashboardDTO
{
    public int Sequence { get; set; }
    public int SubExpenseTypeId { get; set; }
    public string SubExpenseType { get; set; }
    public int ExpenseTypeId { get; set; }
    public string ExpenseType { get; set; }
    public decimal TotalAmount { get; set; }
}
