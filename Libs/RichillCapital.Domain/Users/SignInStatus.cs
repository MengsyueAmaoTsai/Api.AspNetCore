using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Users;

public sealed class SignInStatus : Enumeration<SignInStatus>
{
    public static readonly SignInStatus Success = new SignInStatus(nameof(Success), 0);

    private SignInStatus(string name, int value) :
        base(name, value)
    {
    }
}