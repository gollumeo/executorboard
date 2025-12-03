using ExecutorBoard.Persistence.EF.Configurations;
using ExecutorBoard.Persistence.EF.Records;
using Microsoft.EntityFrameworkCore;

namespace ExecutorBoard.Persistence.EF;

public sealed class ExecutorBoardDbContext(DbContextOptions<ExecutorBoardDbContext> options) : DbContext(options)
{
    public DbSet<UserRecord> Users => Set<UserRecord>();
    public DbSet<EstateRecord> Estates => Set<EstateRecord>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserRecordConfiguration());
        modelBuilder.ApplyConfiguration(new EstateRecordConfiguration());
        modelBuilder.Entity<UserRecord>().Property(ci => ci.Email).HasMaxLength(256);
        modelBuilder.Entity<UserRecord>().Property(ci => ci.PasswordHash).HasMaxLength(256);
    }
}
