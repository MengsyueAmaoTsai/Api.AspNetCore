
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Brokerages;
using RichillCapital.Infrastructure.Brokerages.Rcex;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Brokerages;

public static class BrokerageExtensions
{
    public static IServiceCollection AddBrokerages(this IServiceCollection services)
    {
        services.AddSingleton<BrokerageFactory>();
        services.AddSingleton<IBrokerageManager, BrokerageManager>();

        using var scope = services.BuildServiceProvider().CreateScope();

        List<(string Provider, string Name)> profiles = [
            ("RichillCapital", "RichillCapital"),
        ];

        var factory = scope.ServiceProvider.GetRequiredService<BrokerageFactory>();

        var collection = new BrokerageCollection();
        foreach (var (provider, name) in profiles)
        {
            var result = factory.CreateBrokerage(Guid.NewGuid(), name);

            if (result.IsFailure)
            {
                throw new InvalidOperationException(result.Error.Message);
            }

            collection.Add(result.Value);
        }

        services.AddSingleton<IBrokerageCollection>(collection);

        return services;
    }
}

public interface IBrokerageCollection
{
    IReadOnlyCollection<IBrokerage> All { get; }
    Result Add(IBrokerage brokerage);
}

internal sealed class BrokerageCollection() :
    IBrokerageCollection
{
    private readonly Dictionary<Guid, IBrokerage> _brokerages = [];

    public IReadOnlyCollection<IBrokerage> All => _brokerages.Values;

    public Result Add(IBrokerage brokerage)
    {
        if (_brokerages.ContainsKey(brokerage.Id))
        {
            return Result.Failure(Error.Conflict(
                "Brokerages.AlreadyExists",
                $"Brokerage connection with id {brokerage.Id} already exists."));
        }

        _brokerages.Add(brokerage.Id, brokerage);

        return Result.Success;
    }
}

internal sealed class BrokerageFactory()
{
    internal Result<IBrokerage> CreateBrokerage(
        Guid id,
        string connectionName) =>
        connectionName switch
        {
            "RichillCapital" => Result<IBrokerage>.With(new RcexBrokerage(id, connectionName)),
            _ => Result<IBrokerage>.Failure(Error.Invalid(
                "Brokerages.NotSupported",
                $"Brokerage connection {connectionName} is not supported."))
        };
}

internal sealed class BrokerageManager(
    IBrokerageCollection _brokerages) :
    IBrokerageManager
{
    public IReadOnlyCollection<IBrokerage> ListAll() =>
        _brokerages.All;
}