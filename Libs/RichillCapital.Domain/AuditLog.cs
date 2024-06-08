using RichillCapital.SharedKernel;

namespace RichillCapital.Domain;

public sealed class AuditLog : Entity<AuditLogId>
{
    private AuditLog(AuditLogId id)
        : base(id)
    {
    }
}
