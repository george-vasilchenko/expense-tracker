using Microsoft.EntityFrameworkCore;
using Subscriptions.Domain;

namespace Subscriptions.Infrastructure.DataAccess;

public interface IDataContext
{
    DbSet<Subscription> Subscriptions { get; }
    Task SaveAsync(CancellationToken cancellationToken);
}