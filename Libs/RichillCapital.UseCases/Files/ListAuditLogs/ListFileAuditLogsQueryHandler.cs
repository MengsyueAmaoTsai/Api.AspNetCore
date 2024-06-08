using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.AuditLogs;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Files.ListAuditLogs;

internal sealed class ListFileAuditLogsQueryHandler :
    IQueryHandler<ListFileAuditLogsQuery, ErrorOr<IEnumerable<AuditLogDto>>>
{
    public async Task<ErrorOr<IEnumerable<AuditLogDto>>> Handle(
        ListFileAuditLogsQuery query,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}