using ETL.Orders.DAL.Models;

namespace ETL.Orders.DAL;

public interface IProductRepository
{
    Task<Product?> GetProductByNameAsync(string name);
    Task AddAsync(Product product);
}
