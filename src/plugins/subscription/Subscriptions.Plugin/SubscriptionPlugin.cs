using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Subscriptions.Core;
using Subscriptions.Infrastructure.DataAccess;

namespace Subscriptions.Plugin;

public sealed class SubscriptionPlugin
{
    public static void AddServices(WebApplicationBuilder webApplicationBuilder, string connectionString)
    {
        if (webApplicationBuilder is null) throw new ArgumentNullException(nameof(webApplicationBuilder));

        webApplicationBuilder.Services
            .AddDbContext<SubscriptionDbContext>(o => o.UseSqlServer(connectionString));
    }

    public static void Bootstrap(WebApplication webApplication)
    {
        if (webApplication is null) throw new ArgumentNullException(nameof(webApplication));

        AddEndpoints(webApplication);
        InitializeDatabase(webApplication);
    }

    private static void AddEndpoints(WebApplication webApplication)
    {
        var createSubscription = new CreateSubscription.Endpoint();
        webApplication
            .MapGroup("subscription")
            .MapGroup("v1")
            .WithTags("subscription")
            .MapPost(createSubscription.Pattern, createSubscription.Post);
    }

    private static void InitializeDatabase(IHost webApp)
    {
        using var scope = webApp.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<SubscriptionDbContext>();

        context.Database.Migrate();
    }
}