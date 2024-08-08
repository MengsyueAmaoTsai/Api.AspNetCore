using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Api.Endpoints;
using RichillCapital.Contracts;
using RichillCapital.Contracts.SignalSources;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.SignalSources.Queries;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Src.Endpoints.SignalSources;

[ApiVersion(EndpointVersion.V1)]
public sealed class GetSignalSourceEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<string>
    .WithActionResult<SignalSourceDetailsResponse>
{
    [HttpGet(ApiRoutes.SignalSources.Get)]
    [MapToApiVersion(EndpointVersion.V1)]
    [SwaggerOperation(Tags = ["SignalSources"])]
    public override async Task<ActionResult<SignalSourceDetailsResponse>> HandleAsync(
        [FromRoute(Name = "signalSourceId")] string request,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<string>.With(request)
            .Then(id => new GetSignalSourceQuery { SignalSourceId = id })
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(dto => dto.ToDetailsResponse())
            .Match(HandleFailure, Ok);
}
