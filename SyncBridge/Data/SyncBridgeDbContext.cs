using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SyncBridge.Data
{
    public class SyncBridgeDbContext : DbContext
    {
        public SyncBridgeDbContext(DbContextOptions<SyncBridgeDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<SyncJob> SyncJobs => Set<SyncJob>();
        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<User>()
              .HasIndex(u => u.Email).IsUnique();

            // Store the enum as readable text ("Processing") instead of a number
            mb.Entity<SyncJob>()
              .Property(j => j.Status).HasConversion<string>().HasMaxLength(20);

            // The watchdog will query by status + heartbeat
            mb.Entity<SyncJob>()
              .HasIndex(j => new { j.Status, j.LastHeartbeat });

            mb.Entity<Product>().Property(p => p.Price).HasPrecision(18, 2);

            // IDEMPOTENCY: the same external record can only exist once per user.
            // Running a sync twice updates this row instead of adding a duplicate.
            mb.Entity<Product>()
              .HasIndex(p => new { p.UserId, p.ExternalId }).IsUnique();
        }
    }
}
