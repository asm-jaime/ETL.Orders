using ETL.Orders.Models;

namespace ETL.Orders.Repositories;

class ProductRepository(InternetStoreContext dbContext) : IProductRepository
{
    private readonly InternetStoreContext _dbContext = dbContext;

    public async Task AddAsync(Product product)
    {
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
    }
}
