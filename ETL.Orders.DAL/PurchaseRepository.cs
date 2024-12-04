using ETL.Orders.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ETL.Orders.DAL;

public class PurchaseRepository : IPurchaseRepository
{
    private readonly InternetStoreContext _dbContext;

    public PurchaseRepository(InternetStoreContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Purchase purchase)
    {
        await _dbContext.Purchases.AddAsync(purchase);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Purchase?> GetByNumberOrder(int numberOrder)
    {
        return await _dbContext.Purchases
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.NumberOrder == numberOrder);
    }

    public async Task<Purchase> PutAsync(Purchase purchase)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var existingPurchase = await _dbContext.Purchases
                .Include(p => p.PurchaseItems)
                .SingleOrDefaultAsync(p => p.NumberOrder == purchase.NumberOrder);

            if(existingPurchase != null)
            {
                _dbContext.PurchaseItems.RemoveRange(existingPurchase.PurchaseItems);
                _dbContext.Purchases.Remove(existingPurchase);
                await _dbContext.SaveChangesAsync();
            }

            await _dbContext.Purchases.AddAsync(purchase);
            await _dbContext.SaveChangesAsync();

            await transaction.CommitAsync();

            return purchase;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}

