namespace Subscriptions.Core;

public static class CreateSubscription
{
    public class Handler
    {
        public Task HandleAsync()
        {
            return Task.CompletedTask;
        }
    }

    public class Command
    {
    }

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