using System.Text;

using Asp.Versioning;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Signals;
using RichillCapital.Notifications;
using RichillCapital.SharedKernel.Monads;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Signals;

[ApiVersion(EndpointVersion.V1)]
public sealed class CreateSignalEndpoint(ILineNotifyClient _lineNotification) : AsyncEndpoint
    .WithRequest<CreateSignalRequest>
    .WithActionResult<CreateSignalResponse>
{
    [HttpPost(ApiRoutes.Signals.Create)]
    [MapToApiVersion(EndpointVersion.V1)]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Create a signal.",
        Description = "Create a signal.",
        OperationId = "Signals.Create",
        Tags = ["Signals"])]
    public override async Task<ActionResult<CreateSignalResponse>> HandleAsync(
        [FromBody] CreateSignalRequest request, CancellationToken cancellationToken = default)
    {
        var notificationMessage = SignalNotification
            .CreateBuilder()
            .AppendLine()
            .WithTime(request.Time)
            .WithPositionBehavior(request.PositionBehavior)
            .WithTradeType(request.TradeType)
            .WithSymbol(request.Symbol)
            .WithQuantity(request.Quantity)
            .Build();

        var result = await _lineNotification.NotifyAsync(notificationMessage, cancellationToken);

        return result
            .Match(
                () => Ok(new CreateSignalResponse
                {
                    Id = Guid.NewGuid().ToString(),
                }),
                HandleFailure);
    }
}


internal static class SignalNotification
{
    internal static StringBuilder CreateBuilder() => new();
}

internal static class SignalNotificationMessageBuilderExtensions
{
    internal static StringBuilder WithTime(this StringBuilder builder, DateTimeOffset time) =>
        builder.AppendLine($"Time: {time:yyyy-MM-dd HH:mm:ss.fff zzz}");

    internal static StringBuilder WithPositionBehavior(this StringBuilder builder, string positionBehavior) =>
        builder.AppendLine($"Position Behavior: {positionBehavior}");

    internal static StringBuilder WithTradeType(this StringBuilder builder, string tradeType) =>
        builder.AppendLine($"Trade Type: {tradeType}");

    internal static StringBuilder WithSymbol(this StringBuilder builder, string symbol) =>
        builder.AppendLine($"Symbol: {symbol}");

    internal static StringBuilder WithQuantity(
        this StringBuilder builder,
        decimal quantity) =>
        builder.AppendLine($"Quantity: {quantity}");

    internal static string Build(this StringBuilder builder) => builder.ToString();
}