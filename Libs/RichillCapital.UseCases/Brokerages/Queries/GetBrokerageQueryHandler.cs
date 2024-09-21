using RichillCapital.Domain.Brokerages;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Brokerages.Queries;

internal sealed class GetBrokerageQueryHandler(
    IBrokerageManager _brokerageManager) :
    IQueryHandler<GetBrokerageQuery, ErrorOr<BrokerageDto>>
{
    public async Task<ErrorOr<BrokerageDto>> Handle(
        GetBrokerageQuery query,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(query.ConnectionName))
        {
            return ErrorOr<BrokerageDto>.WithError(Error.Invalid($"{nameof(query.ConnectionName)} is required."));
        }

        var maybeBrokerage = _brokerageManager.GetByName(query.ConnectionName);

        if (maybeBrokerage.IsNull)
        {
            return ErrorOr<BrokerageDto>.WithError(BrokerageErrors.NotFound(query.ConnectionName));
        }

        var brokerage = maybeBrokerage.Value;

        return ErrorOr<BrokerageDto>.With(brokerage.ToDto());
    }
}