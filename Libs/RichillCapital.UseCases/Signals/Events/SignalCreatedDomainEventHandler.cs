﻿using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Signals.Events;

internal sealed class SignalCreatedDomainEventHandler(
    ILogger<SignalCreatedDomainEventHandler> _logger) : 
    IDomainEventHandler<SignalCreatedDomainEvent>
{
    public Task Handle(
        SignalCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Signal created: {domainEvent}", domainEvent);

        return Task.CompletedTask;
    }
}