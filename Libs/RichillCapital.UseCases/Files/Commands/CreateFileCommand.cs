using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Files.Commands;

public sealed record CreateFileCommand : ICommand<Result>
{
}
