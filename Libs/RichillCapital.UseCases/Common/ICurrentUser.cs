namespace RichillCapital.UseCases.Common;

public interface ICurrentUser
{
    string Id { get; }
    bool IsAuthenticated { get; }
}