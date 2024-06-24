using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.SignalSources;
using RichillCapital.SharedKernel.Monads;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.SignalSources;

[ApiVersion(EndpointVersion.V1)]
public sealed class GetSignalSourceEndpoint(IMediator _mediator) : AsyncEndpoint
    .WithRequest<GetSignalSourceRequest>
    .WithActionResult<SignalSourceResponse>
{
    [HttpGet(ApiRoutes.SignalSources.Get)]
    [MapToApiVersion(EndpointVersion.V1)]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Get a signal source by id",
        Description = "Get a signal source by id",
        OperationId = "SignalSources.GetSignalSource",
        Tags = ["SignalSources"])]
    public override async Task<ActionResult<SignalSourceResponse>> HandleAsync(
        [FromRoute] GetSignalSourceRequest request,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<GetSignalSourceRequest>.With(request)
            .Then(req => req.ToQuery())
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(dto => dto.ToResponse())
            .Match(HandleFailure, Ok);
}