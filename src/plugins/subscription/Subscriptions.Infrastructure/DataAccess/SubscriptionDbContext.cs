using Microsoft.EntityFrameworkCore;
using Subscriptions.Domain;

namespace Subscriptions.Infrastructure.DataAccess;

public class SubscriptionDbContext : DbContext
{
    public SubscriptionDbContext(DbContextOptions<SubscriptionDbContext> options)
        : base(options)
    {
    }

    public DbSet<Subscription> Subscriptions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var ap = modelBuilder.Entity<Subscription>();

        ap.Property(o => o.Id)
            .HasConversion(id => id.Value, value => new SubscriptionId(value));
        ap.ToTable(nameof(Subscription));

        base.OnModelCreating(modelBuilder);
    }
}