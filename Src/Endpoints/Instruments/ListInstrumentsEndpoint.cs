using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Instruments;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Instruments.Queries;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Instruments;

[ApiVersion(EndpointVersion.V1)]
public sealed class ListInstrumentsEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithoutRequest
    .WithActionResult<IEnumerable<InstrumentResponse>>
{
    [HttpGet(ApiRoutes.Instruments.List)]
    [SwaggerOperation(Tags = [ApiTags.Instruments])]
    [Authorize]
    public override async Task<ActionResult<IEnumerable<InstrumentResponse>>> HandleAsync(
        CancellationToken cancellationToken = default) =>
        await ErrorOr<ListInstrumentsQuery>
            .With(new ListInstrumentsQuery())
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(instruments => instruments
                .Select(i => i.ToResponse())
                .ToList())
            .Match(HandleFailure, Ok);
}