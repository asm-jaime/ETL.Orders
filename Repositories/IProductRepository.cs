using ETL.Orders.Models;

namespace ETL.Orders.Repositories;

interface IProductRepository
{
    Task AddAsync(Product product);
}
