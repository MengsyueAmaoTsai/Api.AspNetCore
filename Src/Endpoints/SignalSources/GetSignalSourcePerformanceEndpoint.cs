using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.SignalSources;
using RichillCapital.UseCases.SignalSources.Queries;

namespace RichillCapital.Api.Endpoints.SignalSources;

[ApiVersion(EndpointVersion.V1)]
public sealed class GetSignalSourcePerformanceEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<string>
    .WithActionResult<SignalSourcePerformanceDto>
{
    public override async Task<ActionResult<SignalSourcePerformanceDto>> HandleAsync(
        [FromRoute(Name = nameof(signalSourceId))] string signalSourceId,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<string>
            .With(signalSourceId)
            .Then(id => new GetSignalSourcePerformanceQuery
            {
                SignalSourceId = id,
            })
            .Then(query => _mediator.Send(query, cancellationToken))
            .Match(HandleFailure, Ok);
}