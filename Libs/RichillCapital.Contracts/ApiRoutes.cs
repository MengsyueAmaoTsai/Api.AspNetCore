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
        public const string Get = $"{InstrumentsBase}/{{symbol}}";
    }

    public static class WatchLists
    {
        private const string WatchListsBase = $"{ApiBase}/watch-lists";

        public const string List = WatchListsBase;
        public const string Create = WatchListsBase;

        public const string Get = $"{WatchListsBase}/{{watchListId}}";
        public const string Update = $"{WatchListsBase}/{{watchListId}}";
        public const string Delete = $"{WatchListsBase}/{{watchListId}}";
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

        public const string Get = $"{ExecutionsBase}/{{executionId}}";
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

    public static class Brokerages
    {
        private const string BrokeragesBase = $"{ApiBase}/brokerages";

        public const string List = BrokeragesBase;
        public const string Create = BrokeragesBase;

        public const string Get = $"{BrokeragesBase}/{{connectionName}}";
        public const string Delete = $"{BrokeragesBase}/{{connectionName}}";

        public const string Start = $"{BrokeragesBase}/{{connectionName}}/start";
        public const string Stop = $"{BrokeragesBase}/{{connectionName}}/stop";

        public static class Orders
        {
            private const string OrdersBase = $"{BrokeragesBase}/{{connectionName}}/orders";

            public const string Create = OrdersBase;
            public const string List = OrdersBase;
        }
    }

    public static class DataFeeds
    {
        private const string DataFeedsBase = $"{ApiBase}/data-feeds";

        public const string List = DataFeedsBase;
        public const string Create = DataFeedsBase;

        public const string Get = $"{DataFeedsBase}/{{connectionName}}";
        public const string Delete = $"{DataFeedsBase}/{{connectionName}}";

        public const string Start = $"{DataFeedsBase}/{{connectionName}}/start";
        public const string Stop = $"{DataFeedsBase}/{{connectionName}}/stop";

        public static class Instruments
        {
            private const string InstrumentsBase = $"{DataFeedsBase}/{{connectionName}}/instruments";

            public const string List = InstrumentsBase;
        }
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

        public const string List = SignalsBase;
        public const string Create = SignalsBase;
        public const string Get = $"{SignalsBase}/{{signalId}}";
    }

    public static class SignalSubscriptions
    {
        private const string SignalSubscriptionsBase = $"{ApiBase}/signal-subscriptions";

        public const string List = SignalSubscriptionsBase;
        public const string Get = $"{SignalSubscriptionsBase}/{{signalSubscriptionId}}";
    }

    public static class SignalReplicationPolicies
    {
        private const string SignalReplicationPoliciesBase = $"{ApiBase}/signal-replication-policies";

        public const string List = SignalReplicationPoliciesBase;
        public const string Create = SignalReplicationPoliciesBase;

        public const string Get = $"{SignalReplicationPoliciesBase}/{{signalReplicationPolicyId}}";
    }
}

