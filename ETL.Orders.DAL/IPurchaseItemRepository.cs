using ETL.Orders.DAL.Models;

namespace ETL.Orders.DAL;

public interface IPurchaseItemRepository
{
    Task AddRangeAsync(IEnumerable<PurchaseItem> purchaseItems);
}
