using EMPIRIAN.Modules.Users.Services.Users;

namespace EMPIRIAN.Modules.Users;

public static class UsersModule
{
    public static void AddUsersModule(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUsersService, UsersService>();
    }
}