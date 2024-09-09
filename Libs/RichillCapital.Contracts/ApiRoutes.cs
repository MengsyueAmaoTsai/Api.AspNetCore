namespace RichillCapital.Contracts;

public static class ApiRoutes
{
    private const string ApiBase = "api/v{version:apiVersion}";

    public static class Users
    {
        public const string List = $"{ApiBase}/users";
    }
}

