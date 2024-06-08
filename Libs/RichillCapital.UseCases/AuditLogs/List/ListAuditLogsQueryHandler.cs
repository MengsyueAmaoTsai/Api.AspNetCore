
using RichillCapital.Domain;
using RichillCapital.Domain.Common.Repositories;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.AuditLogs.List;

internal sealed class ListAuditLogsQueryHandler(
    IReadOnlyRepository<AuditLog> _auditLogRepository) :
    IQueryHandler<ListAuditLogsQuery, ErrorOr<PagedDto<AuditLogDto>>>
{
    public async Task<ErrorOr<PagedDto<AuditLogDto>>> Handle(
        ListAuditLogsQuery query,
        CancellationToken cancellationToken)
    {
        var logs = await _auditLogRepository.ListAsync(cancellationToken);

        return new PagedDto<AuditLogDto>
        {
            Items = logs
                .Select(log => log.ToDto())
        }.ToErrorOr();
    }
}