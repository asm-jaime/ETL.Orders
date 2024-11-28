using ETL.Orders.DAL.Models;

namespace ETL.Orders.DAL;

public interface IUserRepository
{
    Task<User> GetUserByFirstNameAndLastAsync(string firstName, string lastName);
    Task AddAsync(User user);
}
