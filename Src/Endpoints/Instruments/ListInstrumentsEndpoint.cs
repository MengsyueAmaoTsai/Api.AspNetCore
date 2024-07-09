using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Instruments;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Instruments.List;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Instruments;

[ApiVersion(EndpointVersion.V1)]
public sealed class ListInstrumentsEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithoutRequest
    .WithActionResult<Paged<InstrumentResponse>>
{
    [HttpGet(ApiRoutes.Instruments.List)]
    [MapToApiVersion(EndpointVersion.V1)]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "List instruments",
        Description = "List all instruments available for trading.",
        OperationId = "Instruments.List",
        Tags = ["Instruments"])]
    public override async Task<ActionResult<Paged<InstrumentResponse>>> HandleAsync(
        CancellationToken cancellationToken = default) =>
        await _mediator.Send(new ListInstrumentsQuery(), cancellationToken)
            .Then(dto => dto.ToPagedResponse())
            .Match(HandleFailure, Ok);
}