namespace ETL.Orders.BLL.DTOs;

public class PurchaseItemDTO
{
    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }
}
