using ETL.Orders.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ETL.Orders.DAL;

public class ProductRepository(InternetStoreContext dbContext) : IProductRepository
{
    private readonly InternetStoreContext _dbContext = dbContext;

    public async Task AddAsync(Product product)
    {
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Product?> GetProductByNameAsync(string name)
    {
        return await _dbContext.Products .AsNoTracking().SingleOrDefaultAsync(p => p.ProductName.ToLower() == name.ToLower());
    }
}
