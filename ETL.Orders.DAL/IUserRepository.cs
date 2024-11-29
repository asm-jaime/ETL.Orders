using ETL.Orders.DAL.Models;

namespace ETL.Orders.DAL;

public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email);
    Task AddAsync(User user);
}
