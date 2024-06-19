using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Users;

public sealed class SignInLog : ValueObject
{
    private SignInLog(
        DateTimeOffset time,
        string ipAddress,
        string applicationId,
        SignInStatus status)
    {
        Time = time;
        IpAddress = ipAddress;
        ApplicationId = applicationId;
        Status = status;
    }

    public DateTimeOffset Time { get; private set; }
    public string IpAddress { get; private set; }
    public string ApplicationId { get; private set; }
    public SignInStatus Status { get; private set; }

    public static Result<SignInLog> Create(
        DateTimeOffset time,
        string ipAddress,
        string applicationId,
        SignInStatus status)
    {
        var log = new SignInLog(
            time,
            ipAddress,
            applicationId,
            status);

        return Result<SignInLog>.With(log);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Time;
        yield return Status;
    }
}
