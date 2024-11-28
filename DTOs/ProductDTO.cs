namespace ETL.Orders.DTOs;

public class ProductDTO
{
    public string ProductName { get; set; } = null!;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}
