namespace RichillCapital.Domain.Abstractions;

public interface ICurrentUser
{
    bool IsAuthenticated { get; }
    UserId Id { get; }
    string Name { get; }
    string Email { get; }
}