using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Abstractions;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearDomainEvents(IEnumerable<IEntity> entities);

    Task DispatchAndClearDomainEvents(IEntity entity);
}