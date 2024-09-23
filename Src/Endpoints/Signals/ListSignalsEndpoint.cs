using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Signals;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Signals.Queries;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Signals;

[ApiVersion(EndpointVersion.V1)]
public sealed class ListSignalsEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithoutRequest
    .WithActionResult<IEnumerable<SignalResponse>>
{
    [HttpGet(ApiRoutes.Signals.List)]
    [SwaggerOperation(
        Summary = "List signals",
        Description = "List signals",
        Tags = [ApiTags.Signals])]
    [AllowAnonymous]
    public override async Task<ActionResult<IEnumerable<SignalResponse>>> HandleAsync(
        CancellationToken cancellationToken = default) =>
        await ErrorOr<ListSignalsQuery>
            .With(new ListSignalsQuery())
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(signals => signals
                .Select(dto => dto.ToResponse())
                .ToList())
            .Match(HandleFailure, Ok);
}