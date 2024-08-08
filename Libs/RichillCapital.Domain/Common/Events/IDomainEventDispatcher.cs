using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Common.Events;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearDomainEvents(IEnumerable<IEntity> entities);

    Task DispatchAndClearDomainEvents(IEntity entity);
}