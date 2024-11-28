using ETL.Orders.Models;

namespace ETL.Orders.Repositories;

interface IPurchaseItemRepository
{
    Task AddRangeAsync(IEnumerable<PurchaseItem> purchaseItems);
}
