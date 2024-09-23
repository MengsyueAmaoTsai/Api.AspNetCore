using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Errors;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;
using RichillCapital.UseCases.Trades;

namespace RichillCapital.UseCases.Accounts.Queries;

internal sealed class GetAccountPerformanceQueryHandler(
    IReadOnlyRepository<Account> _accountRepository,
    IReadOnlyRepository<Trade> _tradeRepository,
    IDateTimeProvider _dateTimeProvider) :
    IQueryHandler<GetAccountPerformanceQuery, ErrorOr<AccountPerformanceDto>>
{
    public async Task<ErrorOr<AccountPerformanceDto>> Handle(
        GetAccountPerformanceQuery query,
        CancellationToken cancellationToken)
    {
        var validationResult = AccountId.From(query.AccountId);

        if (validationResult.IsFailure)
        {
            return ErrorOr<AccountPerformanceDto>.WithError(validationResult.Error);
        }

        var id = validationResult.Value;

        if (!await _accountRepository.AnyAsync(a => a.Id == id))
        {
            return ErrorOr<AccountPerformanceDto>.WithError(AccountErrors.NotFound(id));
        }

        var closedTrades = await _tradeRepository.ListAsync(t => t.AccountId == id, cancellationToken);

        return ErrorOr<AccountPerformanceDto>.With(
            new AccountPerformanceDto
            {
                TimeUtc = _dateTimeProvider.UtcNow,
                ClosedTrades = closedTrades
                    .Select(t => t.ToDto())
                    .ToList(),
            });
    }
}