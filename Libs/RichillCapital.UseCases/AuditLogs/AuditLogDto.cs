using RichillCapital.Domain;

namespace RichillCapital.UseCases.AuditLogs;

public sealed record AuditLogDto
{
    public required Guid Id { get; init; }
}

internal static class AuditLogExtensions
{
    public static AuditLogDto ToDto(this AuditLog log) =>
        new()
        {
            Id = log.Id.Value,
        };
}