namespace Subscriptions.Domain;

public class Subscription
{
    public SubscriptionId Id { get; private set; } = null!;
    public SubscriptionPeriod Period { get; private set; } = null!;
    public Money UnitPrice { get; private set; }
    public BillingType Billing { get; private set; }
}