using EMPIRIAN.Database.Users;

namespace EMPIRIAN.Modules.Users.Services.Users;

public interface IUsersService
{
    Task<User> CreatePhantomUser();
}