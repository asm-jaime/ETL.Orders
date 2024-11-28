using ETL.Orders.DAL.Models;

namespace ETL.Orders.DAL;

public class PurchaseRepository(InternetStoreContext dbContext) : IPurchaseRepository
{
    private readonly InternetStoreContext _dbContext = dbContext;

    public async Task AddAsync(Purchase purchase)
    {
        await _dbContext.Purchases.AddAsync(purchase);
        await _dbContext.SaveChangesAsync();
    }

    public Task GetPurchaseByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
