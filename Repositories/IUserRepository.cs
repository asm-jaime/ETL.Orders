using ETL.Orders.Models;

namespace ETL.Orders.Repositories;

interface IUserRepository
{
    Task AddAsync(User user);
}
