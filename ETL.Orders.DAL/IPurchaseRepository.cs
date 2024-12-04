using ETL.Orders.DAL.Models;

namespace ETL.Orders.DAL;

public interface IPurchaseRepository
{
    Task AddAsync(Purchase purchase);
    Task<Purchase> PutAsync(Purchase purchase);
    Task<Purchase?> GetByNumberOrder(int numberOrder);
}
