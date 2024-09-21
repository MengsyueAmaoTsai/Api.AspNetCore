using RichillCapital.Domain.Brokerages;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Brokerages.Commands;

internal sealed class CreateBrokerageOrderCommandHandler(
    IBrokerageManager _brokerageManager) :
    ICommandHandler<CreateBrokerageOrderCommand, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(
        CreateBrokerageOrderCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _brokerageManager.SubmitOrderAsync(cancellationToken);

        if (result.IsFailure)
        {
            return ErrorOr<string>.WithError(result.Error);
        }

        return ErrorOr<string>.With(Guid.NewGuid().ToString());
    }
}