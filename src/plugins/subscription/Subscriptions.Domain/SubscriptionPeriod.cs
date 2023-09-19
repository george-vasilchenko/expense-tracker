namespace Subscriptions.Domain;

public record SubscriptionPeriod(DateTime StartDateUtc, DateTime? EndDateUtc);