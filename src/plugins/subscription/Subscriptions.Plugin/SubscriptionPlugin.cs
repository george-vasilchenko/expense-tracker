using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Subscriptions.Core;
using Subscriptions.Infrastructure.DataAccess;

namespace Subscriptions.Plugin;

public class SubscriptionPlugin
{
    public static void AddServices(WebApplicationBuilder webApplicationBuilder, string connectionString)
    {
        if (webApplicationBuilder is null) throw new ArgumentNullException(nameof(webApplicationBuilder));

        webApplicationBuilder.Services.AddDbContext<SubscriptionDbContext>(o => o.UseSqlServer(connectionString));
    }

    public static void Bootstrap(WebApplication webApplication)
    {
        if (webApplication is null) throw new ArgumentNullException(nameof(webApplication));

        var createSubscription = new CreateSubscription.Endpoint();
        webApplication.MapPost(createSubscription.Pattern, createSubscription.Post);

        InitializeDatabase(webApplication);
    }

    private static void InitializeDatabase(IHost webApp)
    {
        using var scope = webApp.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<SubscriptionDbContext>();

        context.Database.Migrate();
    }
}