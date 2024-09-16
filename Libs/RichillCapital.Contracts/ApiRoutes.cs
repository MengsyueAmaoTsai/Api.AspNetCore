namespace RichillCapital.Contracts;

public static class ApiRoutes
{
    private const string ApiBase = "api/v{version:apiVersion}";

    public static class Users
    {
        public const string List = $"{ApiBase}/users";

        public const string Get = $"{ApiBase}/users/{{userId}}";
    }

    public static class Instruments
    {
        public const string List = $"{ApiBase}/instruments";

        public const string Get = $"{ApiBase}/instruments/{{symbol}}";
    }

    public static class Orders
    {
        public const string List = $"{ApiBase}/orders";
        public const string Create = $"{ApiBase}/orders";
        public const string Get = $"{ApiBase}/orders/{{orderId}}";
    }

    public static class Executions
    {
        public const string List = $"{ApiBase}/executions";
    }

    public static class Positions
    {
        public const string List = $"{ApiBase}/positions";
        public const string Get = $"{ApiBase}/positions/{{positionId}}";
    }

    public static class Trades
    {
        public const string List = $"{ApiBase}/trades";
        public const string Get = $"{ApiBase}/trades/{{tradeId}}";
    }

    public static class SignalSources
    {
        public const string List = $"{ApiBase}/signal-sources";
        public const string Create = $"{ApiBase}/signal-sources";
        public const string Get = $"{ApiBase}/signal-sources/{{signalSourceId}}";
    }

    public static class Signals
    {
        public const string Create = $"{ApiBase}/signals";
    }
}

