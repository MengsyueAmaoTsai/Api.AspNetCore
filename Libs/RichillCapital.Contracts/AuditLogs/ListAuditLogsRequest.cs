using RichillCapital.UseCases.AuditLogs.List;

namespace RichillCapital.Contracts.AuditLogs;

public sealed record ListAuditLogsRequest
{
}

public static class ListAuditLogsRequestMapping
{
    public static ListAuditLogsQuery ToQuery(this ListAuditLogsRequest request) =>
        new();
}