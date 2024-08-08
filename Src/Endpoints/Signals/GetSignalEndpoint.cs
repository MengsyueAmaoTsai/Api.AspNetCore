using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Api.Endpoints;
using RichillCapital.Contracts;
using RichillCapital.Contracts.Signals;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Signals.Queries;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Src.Endpoints.Signals;

[ApiVersion(EndpointVersion.V1)]
public sealed class GetSignalEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<string>
    .WithActionResult<SignalDetailsResponse>
{
    [HttpGet(ApiRoutes.Signals.Get)]
    [MapToApiVersion(EndpointVersion.V1)]
    [SwaggerOperation(Tags = ["Signals"])]
    public override async Task<ActionResult<SignalDetailsResponse>> HandleAsync(
        [FromRoute(Name = "signalId")] string request,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<string>.With(request)
            .Then(id => new GetSignalQuery
            {
                SignalId = id,
            })
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(dto => dto.ToResponse())
            .Match(HandleFailure, Ok);
}
