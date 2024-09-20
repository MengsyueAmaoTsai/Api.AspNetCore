
using RichillCapital.Domain.Brokerages;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Brokerages.Commands;

internal sealed class StartBrokerageCommandHandler(
    IBrokerageManager _brokerageManager) :
    ICommandHandler<StartBrokerageCommand, ErrorOr<BrokerageDto>>
{
    public async Task<ErrorOr<BrokerageDto>> Handle(
        StartBrokerageCommand command,
        CancellationToken cancellationToken)
    {
        var maybeBrokerage = _brokerageManager.GetByName(command.ConnectionName);

        if (maybeBrokerage.IsNull)
        {
            var newBrokerageResult = await _brokerageManager.CreateAndStartAsync(
                command.Provider,
                command.ConnectionName,
                cancellationToken);

            if (newBrokerageResult.IsFailure)
            {
                return ErrorOr<BrokerageDto>.WithError(newBrokerageResult.Error);
            }

            var newBrokerage = newBrokerageResult.Value;
            return ErrorOr<BrokerageDto>.With(newBrokerage.ToDto());
        }

        var brokerage = maybeBrokerage.Value;

        var startResult = await brokerage.StartAsync(cancellationToken);

        if (startResult.IsFailure)
        {
            return ErrorOr<BrokerageDto>.WithError(startResult.Error);
        }

        return ErrorOr<BrokerageDto>.With(brokerage.ToDto());
    }
}