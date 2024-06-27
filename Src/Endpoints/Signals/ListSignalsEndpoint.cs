using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Signals;
using RichillCapital.SharedKernel.Monads;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Signals;

[ApiVersion(EndpointVersion.V1)]
public sealed class ListSignalsEndpoint(IMediator _mediator) : AsyncEndpoint
    .WithRequest<ListRequest>
    .WithActionResult<Paged<SignalResponse>>
{
    [HttpGet(ApiRoutes.Signals.List)]
    [MapToApiVersion(EndpointVersion.V1)]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "List signals",
        Description = "List signals",
        OperationId = "Signals.List",
        Tags = ["Signals"])]

    public override async Task<ActionResult<Paged<SignalResponse>>> HandleAsync(
        [FromQuery] ListRequest request,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<ListRequest>.With(request)
            .Then(req => req.ToQuery())
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(dto => dto.ToPagedResponse())
            .Match(HandleFailure, Ok);
}