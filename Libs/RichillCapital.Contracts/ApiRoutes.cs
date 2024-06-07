namespace RichillCapital.Contracts;

public static class ApiRoutes
{
    private const string ApiBase = "api/v{version:apiVersion}";

    public static class Files
    {
        private const string FileBase = ApiBase + "/files";

        public const string List = FileBase;
        public const string Upload = FileBase;
        public const string Get = FileBase + "/{fileId}";
    }
}