using ETL.Orders.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ETL.Orders.DAL;

public class UserRepository(InternetStoreContext dbContext) : IUserRepository
{
    private readonly InternetStoreContext _dbContext = dbContext;

    public async Task AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _dbContext.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Email == email);
    }
}
