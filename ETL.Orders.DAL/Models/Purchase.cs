namespace ETL.Orders.DAL.Models;

public partial class Purchase
{
    public int PurchaseId { get; set; }

    public int? NumberOrder { get; set; }

    public int UserId { get; set; }

    public DateTime PurchaseDate { get; set; }

    public decimal TotalAmount { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public string ShippingAddress { get; set; } = null!;

    public string BillingAddress { get; set; } = null!;

    public virtual ICollection<PurchaseItem> PurchaseItems { get; set; } = new List<PurchaseItem>();

    public virtual User User { get; set; } = null!;
}
