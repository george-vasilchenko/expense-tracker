namespace Subscriptions.Plugin.Domain;

public record SubscriptionPeriod(DateTime StartDateUtc, DateTime? EndDateUtc);