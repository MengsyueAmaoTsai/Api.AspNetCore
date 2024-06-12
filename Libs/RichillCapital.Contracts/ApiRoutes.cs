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
        public const string Update = FileBase + "/{fileId}";
        public const string Delete = FileBase + "/{fileId}";
        public const string Download = Get + "/download";

        public static class AuditLogs
        {
            private const string FileAuditLogsBase = FileBase + "/{fileId}/audit-logs";

            public const string List = FileAuditLogsBase;
        }
    }

    public static class AuditLogs
    {
        private const string AuditLogsBase = ApiBase + "/audit-logs";

        public const string List = AuditLogsBase;
    }

    public static class Users
    {
        private const string UsersBase = ApiBase + "/users";

        public const string List = UsersBase;
        public const string Get = UsersBase + "/{userId}";
    }

    public static class Signals
    {
        private const string SignalsBase = ApiBase + "/signals";

        public const string Create = SignalsBase;
    }
}