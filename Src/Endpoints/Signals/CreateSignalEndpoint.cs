using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Signals;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Signals.Commands;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Signals;

[ApiVersion(EndpointVersion.V1)]
public sealed class CreateSignalEndpoint(IMediator _mediator) : AsyncEndpoint
    .WithRequest<CreateSignalRequest>
    .WithActionResult<SignalCreatedResponse>
{
    [HttpPost(ApiRoutes.Signals.Create)]
    [SwaggerOperation(Tags = [ApiTags.Signals])]
    [AllowAnonymous]
    public override async Task<ActionResult<SignalCreatedResponse>> HandleAsync(
        [FromBody] CreateSignalRequest request,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<CreateSignalRequest>
            .With(request)
            .Then(req => new CreateSignalCommand
            {
                Time = req.Time,
                Origin = req.Origin,
                SourceId = req.SourceId,
                Symbol = req.Symbol,
                TradeType = req.TradeType,
                OrderType = req.OrderType,
                Quantity = req.Quantity,
            })
            .Then(command => _mediator.Send(command, cancellationToken))
            .Then(id => new SignalCreatedResponse
            {
                Id = id.Value,
            })
            .Match(HandleFailure, Ok);
}