using Microsoft.EntityFrameworkCore;
using Subscriptions.Domain;

namespace Subscriptions.Infrastructure.DataAccess;

public class SubscriptionDbContext : DbContext, IDataContext
{
    public SubscriptionDbContext(DbContextOptions<SubscriptionDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Subscription> Subscriptions { get; private set; } = null!;
    public async Task SaveAsync(CancellationToken cancellationToken)
    {
        await SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var ap = modelBuilder.Entity<Subscription>();

        ap.Property(o => o.Id)
            .HasConversion(
                id => id.Value, 
                value => new SubscriptionId(value));

        ap.OwnsOne(p => p.Period);

        ap.Property(o => o.UnitPrice)
            .HasConversion(
                up => $"{up.WholePart}.{up.DecimalPart}",
                value => Money.FromString(value));

        ap.Property(o => o.Billing).HasConversion<int>();
        
        ap.ToTable(nameof(Subscription));

        base.OnModelCreating(modelBuilder);
    }
}