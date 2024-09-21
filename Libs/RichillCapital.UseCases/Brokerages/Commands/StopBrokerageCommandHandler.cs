
using RichillCapital.Domain.Brokerages;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Brokerages.Commands;

internal sealed class StopBrokerageCommandHandler(
    IBrokerageManager _brokerageManager) :
    ICommandHandler<StopBrokerageCommand, ErrorOr<BrokerageDto>>
{
    public async Task<ErrorOr<BrokerageDto>> Handle(
        StopBrokerageCommand command,
        CancellationToken cancellationToken)
    {
        var brokerageResult = _brokerageManager.GetByName(command.ConnectionName);

        if (brokerageResult.IsFailure)
        {
            return ErrorOr<BrokerageDto>.WithError(brokerageResult.Error);
        }

        var brokerage = brokerageResult.Value;
        var stopResult = await brokerage.StopAsync(cancellationToken);

        if (stopResult.IsFailure)
        {
            return ErrorOr<BrokerageDto>.WithError(stopResult.Error);
        }

        return ErrorOr<BrokerageDto>.With(brokerage.ToDto());
    }
}