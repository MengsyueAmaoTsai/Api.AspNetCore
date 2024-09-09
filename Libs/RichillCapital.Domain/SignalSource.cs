using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class SignalSource : Entity<SignalSourceId>
{
    private SignalSource(
        SignalSourceId id,
        string name,
        string description,
        DateTimeOffset createdTimeUtc)
        : base(id)
    {
        Name = name;
        Description = description;
        CreatedTimeUtc = createdTimeUtc;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTimeOffset CreatedTimeUtc { get; private set; }

    public static ErrorOr<SignalSource> Create(
        SignalSourceId id,
        string name,
        string description,
        DateTimeOffset createdTimeUtc)
    {
        var source = new SignalSource(
            id,
            name,
            description,
            createdTimeUtc);

        return ErrorOr<SignalSource>.With(source);
    }
}
