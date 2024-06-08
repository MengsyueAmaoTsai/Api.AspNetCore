using Microsoft.AspNetCore.Mvc;

using RichillCapital.UseCases.Files.ListAuditLogs;

namespace RichillCapital.Contracts.Files;

public sealed record ListFileAuditLogsRequest
{
    [FromRoute(Name = "fileId")]
    public required Guid FileId { get; init; }
}

public static class ListFileAuditLogsRequestMapping
{
    public static ListFileAuditLogsQuery ToQuery(this ListFileAuditLogsRequest request) =>
        new()
        {
            FileId = request.FileId,
        };
}