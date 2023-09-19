namespace Subscriptions.Core;

public class CreateSubscription
{
    public class Handler
    {
        public Task HandleAsync()
        {
            return Task.CompletedTask;
        }
    }

    public record Command(string Name, DateTime StartDateUtc, DateTime EndDateUtc);

    public class Endpoint
    {
        public string Pattern => "create-subscription";

        public Task Post()
        {
            Console.WriteLine("Create subscription");
            return Task.CompletedTask;
        }
    }
}