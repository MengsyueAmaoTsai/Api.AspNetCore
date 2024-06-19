using RichillCapital.Domain.Users;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class AuditLog : Entity<AuditLogId>
{
    private AuditLog(
        AuditLogId id,
        UserId userId,
        string userName,
        string action,
        string objectId,
        string log,
        DateTimeOffset createdTime)
        : base(id)
    {
        UserId = userId;
        UserName = userName;
        Action = action;
        ObjectId = objectId;
        Log = log;
        CreatedTime = createdTime;
    }

    public UserId UserId { get; private set; }

    public string UserName { get; private set; }

    public string Action { get; private set; }

    public string ObjectId { get; private set; }

    public string Log { get; private set; }

    public DateTimeOffset CreatedTime { get; private set; }

    public static ErrorOr<AuditLog> Create(
        AuditLogId id,
        UserId userId,
        string userName,
        string action,
        string objectId,
        string log,
        DateTimeOffset createdTime)
    {
        var auditLog = new AuditLog(
            id,
            userId,
            userName,
            action,
            objectId,
            log,
            createdTime);

        return auditLog
            .ToErrorOr();
    }
}
