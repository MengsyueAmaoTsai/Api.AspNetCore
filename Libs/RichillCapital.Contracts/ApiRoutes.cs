namespace RichillCapital.Contracts;

public static class ApiRoutes
{
    private const string ApiBase = "api/v{version:apiVersion}";

    public static class SignalSources
    {
        private const string SignalSourcesBase = $"{ApiBase}/signal-sources";

        public const string Create = SignalSourcesBase;
        public const string Get = $"{SignalSourcesBase}/{{signalSourceId}}";
    }

    public static class SignalSourceSubscriptions
    {
        private const string SignalSourceSubscriptionsBase = $"{ApiBase}/signal-source-subscriptions";

        public const string Get = $"{SignalSourceSubscriptionsBase}/{{signalSourceSubscriptionId}}";
    }
}