using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Accounts.Queries;

public sealed record GetAccountPerformanceQuery :
    IQuery<ErrorOr<AccountPerformanceDto>>
{
    public required string AccountId { get; init; }
}
