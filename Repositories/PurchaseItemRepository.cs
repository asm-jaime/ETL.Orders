using ETL.Orders.Models;

namespace ETL.Orders.Repositories;

class PurchaseItemRepository(InternetStoreContext dbContext) : IPurchaseItemRepository
{
    private readonly InternetStoreContext _dbContext = dbContext;

    public async Task AddRangeAsync(IEnumerable<PurchaseItem> purchaseItems)
    {
        await _dbContext.PurchaseItems.AddRangeAsync(purchaseItems);
        await _dbContext.SaveChangesAsync();
    }
}
