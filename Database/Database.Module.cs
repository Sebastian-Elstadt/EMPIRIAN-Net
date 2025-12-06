namespace EMPIRIAN.Database;

public static class DatabaseModule
{
    public static void AddDatabaseModule(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseNpgsql(Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING"));
        });
    }

    public static void MigrateDatabase(this WebApplication app)
    {
        using (IServiceScope serviceScope = app.Services.CreateScope())
        {
            DatabaseContext databaseContext = serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();
            if (databaseContext.Database.GetMigrations().Any()) databaseContext.Database.Migrate();
        }
    }
}