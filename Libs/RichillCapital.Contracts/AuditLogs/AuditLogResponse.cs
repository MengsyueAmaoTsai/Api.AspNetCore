using RichillCapital.UseCases.AuditLogs;
using RichillCapital.UseCases.Common;

namespace RichillCapital.Contracts.AuditLogs;

public record AuditLogResponse
{
}

public static class AuditLogResponseMapping
{
    public static AuditLogResponse ToResponse(this AuditLogDto dto) =>
        new();

    public static Paged<AuditLogResponse> ToResponse(this PagedDto<AuditLogDto> pagedDto) =>
        new()
        {
            Items = pagedDto.Items
                .Select(ToResponse),
        };
}