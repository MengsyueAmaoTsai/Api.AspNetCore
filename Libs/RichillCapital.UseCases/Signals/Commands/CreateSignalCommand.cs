
using RichillCapital.Domain;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Signals.Commands;

public sealed record CreateSignalCommand :
    ICommand<ErrorOr<SignalId>>
{
}
