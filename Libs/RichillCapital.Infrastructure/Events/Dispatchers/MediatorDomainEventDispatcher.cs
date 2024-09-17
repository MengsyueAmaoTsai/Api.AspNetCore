using MediatR;

using RichillCapital.Domain.Abstractions;
using RichillCapital.SharedKernel;

namespace RichillCapital.Infrastructure.Events.Dispatchers;

public sealed class MediatorDomainEventDispatcher(IMediator _mediator) :
    IDomainEventDispatcher
{
    public async Task DispatchAndClearDomainEvents(IEnumerable<IEntity> entities)
    {
        foreach (var entity in entities)
        {
            await DispatchAndClearDomainEvents(entity);
        }
    }

    public async Task DispatchAndClearDomainEvents(IEntity entity)
    {
        var events = entity
            .GetDomainEvents()
            .ToArray();

        entity.ClearDomainEvents();

        foreach (var domainEvent in events)
        {
            await _mediator
                .Publish(domainEvent)
                .ConfigureAwait(false);
        }
    }
}