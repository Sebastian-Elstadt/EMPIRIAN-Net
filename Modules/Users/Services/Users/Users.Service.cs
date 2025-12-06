using EMPIRIAN.Database;
using EMPIRIAN.Database.Users;

namespace EMPIRIAN.Modules.Users.Services.Users;

public class UsersService : IUsersService
{
    private readonly DatabaseContext db;

    public UsersService(DatabaseContext db)
    {
        this.db = db;
    }

    public async Task<User> CreatePhantomUser()
    {
        var user = new User();
        await db.Users.AddAsync(user);
        await db.SaveChangesAsync();
        return user;
    }
}