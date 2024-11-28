using ETL.Orders.Models;

namespace ETL.Orders.Repositories;

class UserRepository(InternetStoreContext dbContext) : IUserRepository
{
    private readonly InternetStoreContext _dbContext = dbContext;

    public async Task AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }
}
