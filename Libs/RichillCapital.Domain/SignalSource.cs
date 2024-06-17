using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class SignalSource : Entity<SignalSourceId>
{
    private readonly List<Signal> _signals = [];

    private SignalSource(
        SignalSourceId id,
        string name,
        string description)
        : base(id)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public IReadOnlyCollection<Signal> Signals => _signals;

    public static ErrorOr<SignalSource> Create(
        SignalSourceId id,
        string name,
        string description)
    {
        var source = new SignalSource(
            id,
            name,
            description);

        return source
            .ToErrorOr();
    }

    public Result AddSignal(Signal signal)
    {
        _signals.Add(signal);

        return Result.Success;
    }
}