using RichillCapital.Domain;

namespace RichillCapital.UseCases.Common;

public interface ICurrentUser
{
    UserId Id { get; }
    bool IsAuthenticated { get; }
}