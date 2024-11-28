namespace ETL.Orders.DTOs;
/*
    OrderNo = orderNo,
    RegistrationDate = registrationDate,
    Sum = orderSum,
    Products = new List<Product>()
 */

public class PurchaseDTO
{
    public int PurchaseId { get; set; }
    public DateTime RegistrationDate { get; set; }
    public decimal TotalAmount { get; set; }
    public ICollection<ProductDTO> Products { get; set; } = [];

    public virtual UserDTO User { get; set; } = null!;
}
