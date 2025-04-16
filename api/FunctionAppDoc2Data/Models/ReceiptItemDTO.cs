namespace FunctionAppDoc2Data.Models;
public class ReceiptItemDTO
{
    public string ItemDescription { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public int SubCategoryId { get; set; }
}
