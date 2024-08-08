namespace RichillCapital.Contracts;

public static class ApiRoutes
{
    private const string ApiBase = "api/v{version:apiVersion}";

    public static class Users
    {
        private const string UsersBase = $"{ApiBase}/users";

        public const string Get = $"{UsersBase}/{{userId}}";
    }

    public static class SignalSources
    {
        private const string SignalSourcesBase = $"{ApiBase}/signal-sources";

        public const string Create = SignalSourcesBase;
        public const string Get = $"{SignalSourcesBase}/{{signalSourceId}}";
    }

    public static class Signals
    {
        private const string SignalsBase = $"{ApiBase}/signals";

        public const string Create = SignalsBase;
        public const string Get = $"{SignalsBase}/{{signalId}}";
    }

    public static class SignalSourceSubscriptions
    {
        private const string SignalSourceSubscriptionsBase = $"{ApiBase}/signal-source-subscriptions";

        public const string Get = $"{SignalSourceSubscriptionsBase}/{{signalSourceSubscriptionId}}";
    }
}