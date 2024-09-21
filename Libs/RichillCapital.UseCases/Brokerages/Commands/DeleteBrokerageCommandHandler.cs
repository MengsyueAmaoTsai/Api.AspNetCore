using RichillCapital.Domain.Brokerages;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Brokerages.Commands;

internal sealed class DeleteBrokerageCommandHandler(
    IBrokerageManager _brokerageManager) :
    ICommandHandler<DeleteBrokerageCommand, Result>
{
    public async Task<Result> Handle(
        DeleteBrokerageCommand command,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(command.ConnectionName))
        {
            return Result.Failure(Error.Invalid($"{nameof(command.ConnectionName)} is required."));
        }

        var brokerageResult = _brokerageManager.GetByName(command.ConnectionName);

        if (brokerageResult.IsFailure)
        {
            return Result.Failure(brokerageResult.Error);
        }

        var brokerage = brokerageResult.Value;

        var removeResult = _brokerageManager.Remove(brokerage);

        if (removeResult.IsFailure)
        {
            return Result.Failure(removeResult.Error);
        }

        return await Task.FromResult(Result.Success);
    }
}