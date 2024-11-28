using ETL.Orders.DAL.Models;

namespace ETL.Orders.DAL;

public class UserRepository(InternetStoreContext dbContext) : IUserRepository
{
    private readonly InternetStoreContext _dbContext = dbContext;

    public async Task AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public Task<User> GetUserByFirstNameAndLastAsync(string firstName, string lastName)
    {
        throw new NotImplementedException();
    }
}
