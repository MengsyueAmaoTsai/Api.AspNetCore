using RichillCapital.Domain.Brokerages;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Brokerages.Commands;

internal sealed class StartBrokerageCommandHandler(
    IBrokerageManager _brokerageManager) :
    ICommandHandler<StartBrokerageCommand, ErrorOr<BrokerageDto>>
{
    public async Task<ErrorOr<BrokerageDto>> Handle(StartBrokerageCommand command, CancellationToken cancellationToken)
    {
        var startResult = await _brokerageManager.StartAsync(command.ConnectionName, cancellationToken);

        if (startResult.IsFailure)
        {
            return ErrorOr<BrokerageDto>.WithError(startResult.Error);
        }

        var brokerage = startResult.Value;

        return ErrorOr<BrokerageDto>.With(brokerage.ToDto());
    }
}
