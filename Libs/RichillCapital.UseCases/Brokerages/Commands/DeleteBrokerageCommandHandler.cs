
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Brokerages.Commands;

internal sealed class DeleteBrokerageCommandHandler :
    ICommandHandler<DeleteBrokerageCommand, Result>
{
    public Task<Result> Handle(DeleteBrokerageCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}