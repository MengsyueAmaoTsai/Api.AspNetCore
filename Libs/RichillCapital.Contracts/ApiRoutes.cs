namespace RichillCapital.Contracts;

public static class ApiRoutes
{
    private const string ApiBase = "api/v{version:apiVersion}";

    public static class SignalSourceSubscriptions
    {
        private const string SignalSourceSubscriptionsBase = $"{ApiBase}/signal-source-subscriptions";

        public const string Get = $"{SignalSourceSubscriptionsBase}/{{signalSourceSubscriptionId}}";
    }
}