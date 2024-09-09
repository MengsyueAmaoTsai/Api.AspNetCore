using MediatR;

namespace RichillCapital.UseCases.Abstractions;

internal interface ICommandHandler<TCommand, TResult> :
    IRequestHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
    new Task<TResult> Handle(
        TCommand command,
        CancellationToken cancellationToken);
}
