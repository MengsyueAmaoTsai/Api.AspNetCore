
using RichillCapital.Domain.Brokerages;
using RichillCapital.SharedKernel;
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
        var maybeBrokerage = _brokerageManager.GetByName(command.ConnectionName);

        if (maybeBrokerage.IsNull)
        {
            return ErrorOr<BrokerageDto>.WithError(Error.NotFound(
                "Brokerages.NotFound",
                $"Brokerage with name {command.ConnectionName} not found"));
        }

        var brokerage = maybeBrokerage.Value;

        var stopResult = await brokerage.StopAsync(cancellationToken);

        if (stopResult.IsFailure)
        {
            return ErrorOr<BrokerageDto>.WithError(stopResult.Error);
        }

        return ErrorOr<BrokerageDto>.With(brokerage.ToDto());
    }
}