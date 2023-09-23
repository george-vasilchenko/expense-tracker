namespace Subscriptions.Domain;

public record SubscriptionId
{
    public Guid Value { get; private set; }

    private SubscriptionId()
    {
    }
    
    public SubscriptionId(Guid value)
    {
        Value = value;
    }

    public static SubscriptionId New()
    {
        return new SubscriptionId
        {
            Value = Guid.NewGuid()
        };
    }
}