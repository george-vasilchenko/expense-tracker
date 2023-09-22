using Microsoft.EntityFrameworkCore;
using Subscriptions.Plugin.Domain;

namespace Subscriptions.Plugin.Infrastructure.DataAccess;

public interface IDataContext
{
    DbSet<Subscription> Subscriptions { get; }
    Task SaveAsync(CancellationToken cancellationToken);
}