
using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Events;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Executions.Events;

internal sealed class ExecutionCreatedDomainEventHandler(
    ILogger<ExecutionCreatedDomainEventHandler> _logger) :
    IDomainEventHandler<ExecutionCreatedDomainEvent>
{
    public Task Handle(
        ExecutionCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Execution created: {ExecutionId}",
            domainEvent.ExecutionId);

        return Task.CompletedTask;
    }
}