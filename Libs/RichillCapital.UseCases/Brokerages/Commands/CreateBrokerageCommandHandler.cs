using RichillCapital.Domain.Brokerages;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Brokerages.Commands;

internal sealed class CreateBrokerageCommandHandler(
    IBrokerageManager _brokerageManager) :
    ICommandHandler<CreateBrokerageCommand, ErrorOr<BrokerageDto>>
{
    public async Task<ErrorOr<BrokerageDto>> Handle(
        CreateBrokerageCommand command,
        CancellationToken cancellationToken)
    {
        var result = _brokerageManager.Create(
            command.Provider,
            command.Name);

        if (result.IsFailure)
        {
            return ErrorOr<BrokerageDto>.WithError(result.Error);
        }

        var brokerage = result.Value;

        return ErrorOr<BrokerageDto>.With(brokerage.ToDto());
    }
}