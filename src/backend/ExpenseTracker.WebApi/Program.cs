using Subscriptions.Configurations;

namespace ExpenseTracker.WebApi;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        SubscriptionPlugin.AddServices(
            builder,
            builder.Configuration.GetConnectionString("SUBSCRIPTIONS_DB_CONTEXT_CONNECTION_STRING")!);

        var app = builder.Build();
        app.UseSwagger();
        app.UseSwaggerUI();

        SubscriptionPlugin.Bootstrap(app);

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}