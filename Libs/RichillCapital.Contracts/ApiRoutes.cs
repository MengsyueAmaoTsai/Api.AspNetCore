namespace RichillCapital.Contracts;

public static class ApiRoutes
{
    private const string ApiBase = "api/v{version:apiVersion}";

    public static class Users
    {
        public const string List = $"{ApiBase}/users";

        public const string Get = $"{ApiBase}/users/{{userId}}";
    }

    public static class Signals
    {
        public const string Create = $"{ApiBase}/signals";
    }
}

