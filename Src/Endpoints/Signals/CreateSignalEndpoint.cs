using System.Text;

using Asp.Versioning;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Signals;
using RichillCapital.Notifications.Discord;
using RichillCapital.Notifications.Line;
using RichillCapital.SharedKernel.Monads;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Signals;

[ApiVersion(EndpointVersion.V1)]
public sealed class CreateSignalEndpoint(
    ILineNotifyClient _lineNotification,
    IDiscordWebhookClient _discordWebhookClient) : AsyncEndpoint
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
        [FromBody] CreateSignalRequest request,
        CancellationToken cancellationToken = default)
    {
        var notificationMessage = SignalNotification
            .CreateBuilder()
            .AppendLine()
            .WithTime(request.Time)
            .AppendLine()
            .WithBehavior(request.Behavior)
            .WithSide(request.Side)
            .WithExchange(request.Exchange)
            .WithSymbol(request.Symbol)
            .WithQuantity(request.Quantity)
            .WithPrice(request.Price)
            .WithOrderType(request.OrderType)
            .Build();

        var result = await _lineNotification.NotifyAsync(notificationMessage, cancellationToken);

        var discordResult = await _discordWebhookClient.SendAsync(
            ApplicationInfo.GetDisplayName(),
            notificationMessage,
            cancellationToken);

        var results = new List<Result> { result, discordResult };

        return results.Any(result => result.IsFailure) ?
            HandleFailure(results.FirstOrDefault(result => result.IsFailure).Error) :
            Ok(new CreateSignalResponse
            {
                Id = Guid.NewGuid().ToString(),
            });
    }
}

internal static class SignalNotification
{
    internal static StringBuilder CreateBuilder() => new();
}

internal static class SignalNotificationMessageBuilderExtensions
{
    internal static StringBuilder WithTime(this StringBuilder builder, DateTimeOffset time) =>
        builder.Append($"Time: {time:yyyy-MM-dd HH:mm:ss.fff zzz}");

    internal static StringBuilder WithBehavior(this StringBuilder builder, string behavior) =>
        builder.Append($"{behavior} ");

    internal static StringBuilder WithSide(this StringBuilder builder, string side) =>
        builder.Append($"{side} ");

    internal static StringBuilder WithExchange(this StringBuilder builder, string exchange) =>
        builder.Append($"{exchange}:");

    internal static StringBuilder WithSymbol(this StringBuilder builder, string symbol) =>
        builder.Append($"{symbol} ");

    internal static StringBuilder WithQuantity(
        this StringBuilder builder,
        decimal quantity) =>
        builder.Append($"{quantity}");

    internal static StringBuilder WithPrice(this StringBuilder builder, decimal price) =>
        builder.Append($"@{price} ");

    internal static StringBuilder WithOrderType(this StringBuilder builder, string orderType) =>
        builder.Append(orderType);

    internal static string Build(this StringBuilder builder) => builder.ToString();
}