namespace ETL.Orders.BLL.DTOs;

public class PurchaseDTO
{
    public int NumberOrder { get; set; }

    public DateTime PurchaseDate { get; set; }

    public decimal TotalAmount { get; set; }

    public ICollection<PurchaseItemDTO> PurchaseItems { get; set; } = [];

    public virtual UserDTO User { get; set; } = null!;
}
