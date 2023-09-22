using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Subscriptions.Plugin.Features;
using Subscriptions.Plugin.Infrastructure.DataAccess;
using WebApi.Plugin.Core;

namespace Subscriptions.Plugin.Configurations;

public static class SubscriptionPlugin
{
    public static void AddServices(WebApplicationBuilder webApplicationBuilder, string connectionString)
    {
        if (webApplicationBuilder is null) throw new ArgumentNullException(nameof(webApplicationBuilder));

        webApplicationBuilder.Services
            .AddScoped<IDataContext, SubscriptionDbContext>()
            .AddScoped<IHandler<CreateSubscriptionCommand, CreateSubscriptionResult>, CreateSubscriptionHandler>();
        webApplicationBuilder.Services
            .AddDbContext<SubscriptionDbContext>(o => o.UseSqlServer(connectionString));
    }

    public static void Bootstrap(WebApplication webApplication)
    {
        if (webApplication is null) throw new ArgumentNullException(nameof(webApplication));

        AddEndpoints(webApplication);
        InitializeDatabase(webApplication);
    }

    private static void AddEndpoints(IEndpointRouteBuilder webApplication)
    {
        webApplication
            .MapGroup("subscription")
            .MapGroup("v1")
            .WithTags("Subscriptions")
            .MapPost(CreateSubscriptionEndpoint.Pattern, CreateSubscriptionEndpoint.Post);
    }

    private static void InitializeDatabase(IHost webApp)
    {
        using var scope = webApp.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<SubscriptionDbContext>();

        context.Database.Migrate();
    }
}