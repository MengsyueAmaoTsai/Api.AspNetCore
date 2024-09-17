using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Account : Entity<AccountId>
{
    private readonly List<Order> _orders = [];

    private Account(
        AccountId id,
        UserId userId,
        string alias,
        string currency,
        DateTimeOffset createdTimeUtc) : base(id)
    {
        UserId = userId;
        Alias = alias;
        Currency = currency;
        CreatedTimeUtc = createdTimeUtc;
    }

    public UserId UserId { get; private set; }
    public string Alias { get; private set; }
    public string Currency { get; private set; }
    public DateTimeOffset CreatedTimeUtc { get; private set; }

    public User User { get; private set; }
    public IReadOnlyCollection<Order> Orders => _orders;

    public static ErrorOr<Account> Create(
        AccountId id,
        UserId userId,
        string alias,
        string currency,
        DateTimeOffset createdTimeUtc)
    {
        var account = new Account(
            id,
            userId,
            alias,
            currency,
            createdTimeUtc);

        return ErrorOr<Account>.With(account);
    }
}
