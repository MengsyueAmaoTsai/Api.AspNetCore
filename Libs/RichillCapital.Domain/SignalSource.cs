using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class SignalSource : Entity<SignalSourceId>
{
    private readonly List<Signal> _signals = [];
    private readonly List<SignalSourceSubscription> _subscriptions = [];

    private SignalSource(SignalSourceId id)
        : base(id)
    {
    }

    #region Navigation Properties

    public IReadOnlyCollection<Signal> Signals => _signals;

    public IReadOnlyCollection<SignalSourceSubscription> Subscriptions => _subscriptions;

    #endregion

    public static ErrorOr<SignalSource> Create(SignalSourceId id)
    {
        var source = new SignalSource(id);

        return ErrorOr<SignalSource>.With(source);
    }
}

public sealed class SignalSourceId : SingleValueObject<string>
{
    internal const int MaxLength = 36;

    private SignalSourceId(string value)
        : base(value)
    {
    }

    public static Result<SignalSourceId> From(string value) =>
        Result<string>
            .With(value)
            .Then(id => new SignalSourceId(id));
}