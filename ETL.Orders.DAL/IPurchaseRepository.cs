using ETL.Orders.DAL.Models;

namespace ETL.Orders.DAL;

public interface IPurchaseRepository
{
    Task GetPurchaseByIdAsync(int id);
    Task AddAsync(Purchase purchase);
}
