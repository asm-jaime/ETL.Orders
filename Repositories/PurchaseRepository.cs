using ETL.Orders.Models;

namespace ETL.Orders.Repositories;

class PurchaseRepository(InternetStoreContext dbContext) : IPurchaseRepository
{
    private readonly InternetStoreContext _dbContext = dbContext;

    public async Task AddAsync(Purchase purchase)
    {
        await _dbContext.Purchases.AddAsync(purchase);
        await _dbContext.SaveChangesAsync();
    }
}
