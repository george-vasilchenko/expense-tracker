namespace Subscriptions.Domain;

public class Subscription
{
    private Subscription()
    {
    }

    public string Name { get; private set; } = null!;
    public SubscriptionId Id { get; private set; } = null!;
    public SubscriptionPeriod Period { get; private set; } = null!;
    public Money UnitPrice { get; private set; }
    public BillingType Billing { get; private set; }

    public static Subscription New(string name, SubscriptionPeriod period, Money unitPrice, BillingType billing)
    {
        if (period is null) throw new ArgumentNullException(nameof(period));

        return new Subscription
        {
            Id = SubscriptionId.New(),
            Name = name,
            Period = period,
            UnitPrice = unitPrice,
            Billing = billing
        };
    }
}