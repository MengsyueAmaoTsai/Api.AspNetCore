using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Email : SingleValueObject<string>
{
    internal const int MaxLength = 256;

    private Email(string value)
        : base(value)
    {
    }

    public static Result<Email> From(string value) =>
        Result<string>
            .With(value)
            .Then(email => new Email(email));
}