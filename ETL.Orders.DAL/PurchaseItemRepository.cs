using ETL.Orders.DAL.Models;

namespace ETL.Orders.DAL;

public class PurchaseItemRepository(InternetStoreContext dbContext) : IPurchaseItemRepository
{
    private readonly InternetStoreContext _dbContext = dbContext;

    public async Task AddRangeAsync(IEnumerable<PurchaseItem> purchaseItems)
    {
        await _dbContext.PurchaseItems.AddRangeAsync(purchaseItems);
        await _dbContext.SaveChangesAsync();
    }
}
