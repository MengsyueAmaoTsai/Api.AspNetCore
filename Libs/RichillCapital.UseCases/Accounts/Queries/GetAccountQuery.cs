using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Accounts.Queries;

public sealed record GetAccountQuery : IQuery<ErrorOr<AccountDto>>
{
    public required string AccountId { get; init; }
}
