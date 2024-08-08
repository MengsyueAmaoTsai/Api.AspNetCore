using RichillCapital.Domain;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Persistence.Seeds;

internal static partial class Seed
{
    internal static IEnumerable<User> CreateUsers()
    {
        yield return CreateUser("UID0000001");
    }

    internal static User CreateUser(string id) =>
        User.Create(
            UserId.From(id).ThrowIfFailure().ValueOrDefault)
        .ThrowIfError()
        .ValueOrDefault;
}