namespace RichillCapital.Contracts;

public static class ApiRoutes
{
    private const string ApiBase = "api/v{version:apiVersion}";

    public static class Users
    {
        private const string UsersBase = $"{ApiBase}/users";

        public const string List = UsersBase;
        public const string Get = $"{UsersBase}/{{userId}}";
    }

    public static class Instruments
    {
        private const string InstrumentsBase = $"{ApiBase}/instruments";

        public const string List = InstrumentsBase;
        public const string Get = $"{InstrumentsBase}/{{instrumentId}}";
    }

    public static class WatchLists
    {
        private const string WatchListsBase = $"{ApiBase}/watch-lists";

        public const string List = WatchListsBase;
        public const string Create = WatchListsBase;
    }

    public static class Accounts
    {
        private const string AccountsBase = $"{ApiBase}/accounts";

        public const string List = AccountsBase;
        public const string Get = $"{AccountsBase}/{{accountId}}";
    }

    public static class Orders
    {
        private const string OrdersBase = $"{ApiBase}/orders";

        public const string List = OrdersBase;
        public const string Create = OrdersBase;
        public const string Get = $"{OrdersBase}/{{orderId}}";
    }

    public static class Executions
    {
        private const string ExecutionsBase = $"{ApiBase}/executions";

        public const string List = ExecutionsBase;
    }

    public static class Positions
    {
        private const string PositionsBase = $"{ApiBase}/positions";

        public const string List = PositionsBase;
        public const string Get = $"{PositionsBase}/{{positionId}}";
    }

    public static class Trades
    {
        private const string TradesBase = $"{ApiBase}/trades";

        public const string List = TradesBase;
        public const string Get = $"{TradesBase}/{{tradeId}}";
    }

    public static class SignalSources
    {
        private const string SignalSourcesBase = $"{ApiBase}/signal-sources";

        public const string List = SignalSourcesBase;
        public const string Create = SignalSourcesBase;
        public const string Get = $"{SignalSourcesBase}/{{signalSourceId}}";
    }

    public static class Signals
    {
        private const string SignalsBase = $"{ApiBase}/signals";

        public const string Create = SignalsBase;
    }
}

