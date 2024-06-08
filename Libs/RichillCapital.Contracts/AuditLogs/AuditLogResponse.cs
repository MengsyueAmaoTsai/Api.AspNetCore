using RichillCapital.UseCases.AuditLogs;

namespace RichillCapital.Contracts.AuditLogs;

public record AuditLogResponse
{
}

public static class AuditLogResponseMapping
{
    public static AuditLogResponse ToResponse(this AuditLogDto dto) =>
        new();
}