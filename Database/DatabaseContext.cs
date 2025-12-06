namespace EMPIRIAN.Database;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasPostgresExtension("uuid-ossp");
        builder.Entity<Users.User>().Property(x => x.ID).HasDefaultValueSql("uuid_generate_v4()").IsRequired();
        builder.Entity<Signalling.SignallingBridge>().Property(x => x.ID).HasDefaultValueSql("uuid_generate_v4()").IsRequired();

        base.OnModelCreating(builder);
    }

    public DbSet<Users.User> Users { get; set; }
    public DbSet<Signalling.SignallingBridge> SignallingBridges { get; set; }
}