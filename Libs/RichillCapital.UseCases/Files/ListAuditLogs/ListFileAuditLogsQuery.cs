using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.AuditLogs;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Files.ListAuditLogs;

public sealed record ListFileAuditLogsQuery :
    IQuery<ErrorOr<IEnumerable<AuditLogDto>>>
{
    public required Guid FileId { get; init; }
}
