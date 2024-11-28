using ETL.Orders.DAL.Models;

namespace ETL.Orders.DAL;

public class ProductRepository(InternetStoreContext dbContext) : IProductRepository
{
    private readonly InternetStoreContext _dbContext = dbContext;

    public async Task AddAsync(Product product)
    {
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
    }

    public Task<Product> GetProductByNameAsync(string name)
    {
        throw new NotImplementedException();
    }
}
