using MediatR;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace RichillCapital.Infrastructure.Events;

internal sealed class DomainEventProcessorJob(
    ILogger<DomainEventProcessorJob> _logger,
    IServiceProvider _serviceProvider,
    InMemoryDomainEventQueue _eventQueue) :
    BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        await foreach (var domainEvent in _eventQueue.Reader.ReadAllAsync(stoppingToken))
        {
            await _mediator.Publish(domainEvent);

            _logger.LogDebug("Processed domain event: {eventType}", domainEvent.GetType().Name);
        }
    }
}