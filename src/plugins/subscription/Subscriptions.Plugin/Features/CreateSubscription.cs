using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Subscriptions.Plugin.Domain;
using Subscriptions.Plugin.Infrastructure.DataAccess;
using WebApi.Plugin.Core;

namespace Subscriptions.Plugin.Features;

public class CreateSubscriptionHandler : IHandler<CreateSubscriptionCommand, CreateSubscriptionResult>
{
    private readonly IDataContext _dataContext;

    public CreateSubscriptionHandler(IDataContext dataContext) => _dataContext = dataContext;

    public async Task<CreateSubscriptionResult> HandleAsync(
        CreateSubscriptionCommand command,
        CancellationToken cancellationToken = default)
    {
        if (command is null) throw new ArgumentNullException(nameof(command));

        var period = new SubscriptionPeriod(command.StartDateUtc, command.EndDateUtc);
        var unitPrice = new Money(command.UnitPriceWholePart, command.UnitPriceDecimalPart);
        var subscription = Subscription.New(command.Name, period, unitPrice, command.Billing);
        var entry = await _dataContext.Subscriptions.AddAsync(subscription, cancellationToken)
            .ConfigureAwait(false);
        await _dataContext.SaveAsync(cancellationToken)
            .ConfigureAwait(false);
        
        return new CreateSubscriptionResult(entry.Entity.Id.Value);
    }
}

public record CreateSubscriptionCommand(
    string Name,
    DateTime StartDateUtc,
    DateTime EndDateUtc,
    int UnitPriceWholePart,
    int UnitPriceDecimalPart,
    BillingType Billing);

public record CreateSubscriptionResult(Guid Id);

public static class CreateSubscriptionEndpoint
{
    public static string Pattern => "create-subscription";

    public static async Task<CreateSubscriptionResult> Post(
        [AsParameters] CreateSubscriptionCommand command,
        [FromServices] IHandler<CreateSubscriptionCommand, CreateSubscriptionResult> handler,
        CancellationToken cancellationToken)
    {
        return await handler.HandleAsync(command, cancellationToken).ConfigureAwait(false);
    }
}