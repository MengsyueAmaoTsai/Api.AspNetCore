
using RichillCapital.Domain;
using RichillCapital.Domain.Common.Repositories;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.AuditLogs.List;

public sealed record ListAuditLogsQuery :
    IQuery<ErrorOr<PagedDto<AuditLogDto>>>
{
}

internal sealed class ListAuditLogsQueryHandler(
    IReadOnlyRepository<AuditLog> _auditLogRepository) :
    IQueryHandler<ListAuditLogsQuery, ErrorOr<PagedDto<AuditLogDto>>>
{
    public Task<ErrorOr<PagedDto<AuditLogDto>>> Handle(
        ListAuditLogsQuery query,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}