using Asp.Versioning;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Signals;
using RichillCapital.Notifications.Discord;
using RichillCapital.Notifications.Line;
using RichillCapital.SharedKernel;
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
        var behavior = "None";
        var side = "None";

        if (request.MarketPositionSize == request.PreviousMarketPositionSize)
        {
            return HandleFailure(Error.Invalid("Market position size is the same as the previous market position size."));
        }

        if (request.MarketPositionSize > request.PreviousMarketPositionSize)
        {
            if (request.PreviousMarketPosition == "flat")
            {
                if (request.MarketPosition == "long")
                {
                    behavior = "Enter";
                    side = "Long";
                }

                if (request.MarketPosition == "short")
                {
                    behavior = "Enter";
                    side = "Short";
                }
            }

            if (request.PreviousMarketPosition == "long" && request.MarketPosition == "long")
            {
                behavior = "Increase";
                side = "Long";
            }

            if (request.PreviousMarketPosition == "short" && request.MarketPosition == "short")
            {
                behavior = "Increase";
                side = "Short";
            }
        }

        if (request.MarketPositionSize < request.PreviousMarketPositionSize)
        {
            if (request.PreviousMarketPosition == "long" && request.MarketPosition == "flat")
            {
                behavior = "Exit";
                side = "Long";
            }

            if (request.PreviousMarketPosition == "short" && request.MarketPosition == "flat")
            {
                behavior = "Exit";
                side = "Short";
            }

            if (request.PreviousMarketPosition == "long" && request.MarketPosition == "long")
            {
                behavior = "Decrease";
                side = "Long";
            }

            if (request.PreviousMarketPosition == "short" && request.MarketPosition == "short")
            {
                behavior = "Decrease";
                side = "Short";
            }
        }

        var notificationMessage = "";
        notificationMessage += $"Time: {request.CurrentTime:yyyy-MM-dd HH:mm:ss.fff zzz}\n";
        notificationMessage += $"Source: {request.SourceId}\n";
        notificationMessage += $"{behavior} {side} {request.Symbol} {request.Price}\n";

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