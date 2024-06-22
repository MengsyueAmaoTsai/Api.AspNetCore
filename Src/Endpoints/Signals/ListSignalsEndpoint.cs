using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Signals;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Signals.List;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Signals;

[ApiVersion(EndpointVersion.V1)]
public sealed class ListSignalsEndpoint(IMediator _mediator) : AsyncEndpoint
    .WithoutRequest
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
        CancellationToken cancellationToken = default) =>
        await _mediator.Send(new ListSignalsQuery(), cancellationToken)
            .Then(dto => dto.ToPagedResponse())
            .Match(HandleFailure, Ok);
}