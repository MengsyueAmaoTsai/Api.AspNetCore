namespace RichillCapital.Contracts;

public static class ApiRoutes
{
    private const string ApiBase = "api/v{version:apiVersion}";

    public static class Users
    {
        private const string UsersBase = $"{ApiBase}/users";

        public const string List = UsersBase;
        public const string Create = UsersBase;
        public const string Get = $"{UsersBase}/{{userId}}";
    }

    public static class Accounts
    {
        private const string AccountsBase = $"{ApiBase}/accounts";

        public const string List = AccountsBase;
    }

    public static class Orders
    {
        private const string OrdersBase = $"{ApiBase}/orders";

        public const string Create = OrdersBase;
    }

    public static class Instruments
    {
        private const string InstrumentsBase = $"{ApiBase}/instruments";

        public const string List = InstrumentsBase;
    }

    public static class SignalSources
    {
        private const string SignalSourceBase = $"{ApiBase}/signal-sources";

        public const string List = SignalSourceBase;
        public const string Create = SignalSourceBase;
        public const string Get = $"{SignalSourceBase}/{{signalSourceId}}";
    }

    public static class Signals
    {
        private const string SignalsBase = ApiBase + "/signals";

        public const string List = SignalsBase;
        public const string Create = SignalsBase;
    }
}