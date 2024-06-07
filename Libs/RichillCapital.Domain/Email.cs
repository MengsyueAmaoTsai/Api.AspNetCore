using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Email : SingleValueObject<string>
{
    public const int MaxLength = 100;

    private Email(string value)
        : base(value)
    {
    }

    public static Result<Email> From(string value) => value
        .ToResult()
        .Then(email => new Email(email));
}