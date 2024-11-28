using ETL.Orders.DAL;

namespace ETL.Orders.BLL.Services;

public class UserService(IUserRepository userRepository)
{
    IUserRepository _userRepository = userRepository;
}
