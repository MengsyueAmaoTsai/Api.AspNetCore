using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.AuditLogs.List;

public sealed record ListAuditLogsQuery :
    IQuery<ErrorOr<PagedDto<AuditLogDto>>>
{
}
