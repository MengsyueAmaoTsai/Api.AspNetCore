using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class AuditLogId : SingleValueObject<Guid>
{
    public const int MaxLength = 36;

    private AuditLogId(Guid value)
        : base(value)
    {
    }

    public static Result<AuditLogId> From(Guid value) => value
        .ToResult()
        .Then(id => new AuditLogId(id));
}