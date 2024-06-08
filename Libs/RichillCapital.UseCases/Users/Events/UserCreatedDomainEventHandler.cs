
using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Users.Events;

internal sealed class UserCreatedDomainEventHandler(
    ILogger<UserCreatedDomainEventHandler> _logger) :
    IDomainEventHandler<UserCreatedDomainEvent>
{
    public Task Handle(
        UserCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogWarning("User created.");

        return Task.CompletedTask;
    }
}