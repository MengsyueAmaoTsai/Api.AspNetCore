using Microsoft.Extensions.DependencyInjection;

using RichillCapital.Domain.Abstractions;
using RichillCapital.Infrastructure.Events.Dispatchers;

namespace RichillCapital.Infrastructure.Events;

public static class EventExtensions
{
    public static IServiceCollection AddDomainEvents(this IServiceCollection services)
    {
        services.AddSingleton<InMemoryDomainEventQueue>();

        services.AddSingleton<IDomainEventBus, DomainEventBus>();

        services.AddScoped<IDomainEventDispatcher, InMemoryChannelDomainEventDispatcher>();

        services.AddHostedService<DomainEventProcessorJob>();

        return services;
    }
}