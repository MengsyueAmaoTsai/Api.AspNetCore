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
public sealed class GetSignalEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<string>
    .WithActionResult<SignalDetailsResponse>
{
    [HttpGet(ApiRoutes.Signals.Get)]
    [SwaggerOperation(
        Summary = "Get signal by id",
        Description = "Get signal by id",
        Tags = [ApiTags.Signals])]
    [AllowAnonymous]
    public override async Task<ActionResult<SignalDetailsResponse>> HandleAsync(
        [FromRoute(Name = nameof(signalId))] string signalId,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<string>
            .With(signalId)
            .Then(id => new GetSignalQuery
            {
                SignalId = id,
            })
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(dto => dto.ToDetailsResponse())
            .Match(HandleFailure, Ok);
}