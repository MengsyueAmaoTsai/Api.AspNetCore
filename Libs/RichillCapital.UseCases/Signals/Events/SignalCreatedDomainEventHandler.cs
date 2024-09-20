using System.Text;

using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Events;
using RichillCapital.UseCases.Abstractions;

internal sealed class SignalCreatedDomainEventHandler(
    ILogger<SignalCreatedDomainEventHandler> _logger,
    ILineNotificationService _lineNotification) :
    IDomainEventHandler<SignalCreatedDomainEvent>
{
    private const string HardCodeToken = "rTjS0liSNNJSzAtbvYb5YfdyPUazxszoG65nrf9rtC1";
    public async Task Handle(
        SignalCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "[SignalCreated] {time} signal of source {sourceId} from origin: {origin}",
            domainEvent.Time,
            domainEvent.SourceId,
            domainEvent.Origin);

        var message = new StringBuilder()
            .AppendLine($"Time: {domainEvent.Time}")
            .AppendLine($"SourceId: {domainEvent.SourceId}")
            .AppendLine($"Origin: {domainEvent.Origin}")
            .ToString();

        var result = await _lineNotification.SendAsync(
            HardCodeToken,
            message,
            cancellationToken);

        if (result.IsFailure)
        {
            _logger.LogError(
                "Failed to send Line notification: {message}",
                result.Error.Message);
        }
    }
}