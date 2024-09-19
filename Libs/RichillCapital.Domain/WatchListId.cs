using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class WatchListId : SingleValueObject<string>
{
    internal const int MaxLength = 36;

    private WatchListId(string value)
        : base(value)
    {
    }

    public static Result<WatchListId> From(string value) =>
        Result<string>
            .With(value)
            .Ensure(id => !string.IsNullOrWhiteSpace(id), Error.Invalid($"'{nameof(value)}' cannot be empty."))
            .Ensure(id => id.Length <= MaxLength, Error.Invalid($"'{nameof(value)}' cannot be longer than {MaxLength} characters."))
            .Then(id => new WatchListId(id));

    public static WatchListId NewWatchListId() =>
        From(Guid.NewGuid().ToString()).ThrowIfFailure().Value;
}