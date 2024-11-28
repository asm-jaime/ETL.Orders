using ETL.Orders.Models;

namespace ETL.Orders.Repositories;

interface IPurchaseRepository
{
    Task AddAsync(Purchase purchase);
}
