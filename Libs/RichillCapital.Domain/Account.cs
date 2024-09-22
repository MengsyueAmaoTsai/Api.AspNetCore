using RichillCapital.Domain.Events;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Account : Entity<AccountId>
{
    private readonly List<Order> _orders = [];
    private readonly List<Execution> _executions = [];
    private readonly List<Position> _positions = [];
    private readonly List<Trade> _trades = [];

    private Account(
        AccountId id,
        UserId userId,
        string connectionName,
        string alias,
        Currency currency,
        DateTimeOffset createdTimeUtc) : base(id)
    {
        UserId = userId;
        ConnectionName = connectionName;
        Alias = alias;
        Currency = currency;
        CreatedTimeUtc = createdTimeUtc;
    }

    public UserId UserId { get; private set; }
    public string ConnectionName { get; private set; }
    public string Alias { get; private set; }
    public Currency Currency { get; private set; }
    public DateTimeOffset CreatedTimeUtc { get; private set; }

    public IReadOnlyList<Order> Orders => _orders;
    public IReadOnlyList<Execution> Executions => _executions;
    public IReadOnlyList<Position> Positions => _positions;
    public IReadOnlyList<Trade> Trades => _trades;

    public static ErrorOr<Account> Create(
        AccountId accountId,
        UserId userId,
        string connectionName,
        string alias,
        Currency currency,
        DateTimeOffset createdTimeUtc)
    {
        var account = new Account(
            accountId,
            userId,
            connectionName,
            alias,
            currency,
            createdTimeUtc);

        account.RegisterDomainEvent(new AccountCreatedDomainEvent
        {
            AccountId = account.Id,
        });

        return ErrorOr<Account>.With(account);
    }
}