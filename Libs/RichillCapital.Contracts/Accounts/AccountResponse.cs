using RichillCapital.UseCases.Accounts;
using RichillCapital.UseCases.Common;

namespace RichillCapital.Contracts.Accounts;

public sealed record AccountResponse
{
    public required string Id { get; init; }

    public required string UserId { get; init; }

    public required string Name { get; init; }

    public required DateTimeOffset CreatedAt { get; init; }
}

public static class AccountResponseMapping
{
    public static AccountResponse ToResponse(this AccountDto account) =>
        new()
        {
            Id = account.Id,
            UserId = account.UserId,
            Name = account.Name,
            CreatedAt = account.CreatedAt,
        };

    public static Paged<AccountResponse> ToPagedResponse(this PagedDto<AccountDto> pagedDto) =>
        new()
        {
            Items = pagedDto.Items.Select(ToResponse),
            TotalCount = pagedDto.TotalCount,
        };
}