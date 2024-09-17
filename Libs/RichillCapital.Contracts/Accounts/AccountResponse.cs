using RichillCapital.UseCases.Accounts;

namespace RichillCapital.Contracts.Accounts;

public record AccountResponse
{
    public required string Id { get; init; }
    public required string UserId { get; init; }
    public required string Alias { get; init; }
    public required string Currency { get; init; }
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}

public sealed record AccountDetailsResponse : AccountResponse
{
}

public static class AccountResponseMapping
{
    public static AccountResponse ToResponse(this AccountDto dto) =>
        new()
        {
            Id = dto.Id,
            UserId = dto.UserId,
            Alias = dto.Alias,
            Currency = dto.Currency,
            CreatedTimeUtc = dto.CreatedTimeUtc,
        };

    public static AccountDetailsResponse ToDetailsResponse(this AccountDto dto) =>
        new()
        {
            Id = dto.Id,
            UserId = dto.UserId,
            Alias = dto.Alias,
            Currency = dto.Currency,
            CreatedTimeUtc = dto.CreatedTimeUtc,
        };
}
