using RichillCapital.UseCases.Common;

namespace RichillCapital.Identity;

internal sealed class FakeUser() : ICurrentUser
{
    public bool IsAuthenticated => true;

    public string Id => "1";
}