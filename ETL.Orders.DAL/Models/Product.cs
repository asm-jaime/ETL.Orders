namespace ETL.Orders.DAL.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public string? Category { get; set; }

    public DateTime DateAdded { get; set; }

    public virtual ICollection<PurchaseItem> PurchaseItems { get; set; } = new List<PurchaseItem>();
}
