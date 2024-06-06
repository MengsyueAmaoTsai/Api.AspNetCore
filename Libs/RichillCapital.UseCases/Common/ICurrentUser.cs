namespace RichillCapital.UseCases.Common;

public interface ICurrentUser
{
    bool IsAuthenticated { get; }
}