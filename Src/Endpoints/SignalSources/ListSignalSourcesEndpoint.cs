using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.SignalSources;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.SignalSources.Queries;

namespace RichillCapital.Api.Endpoints.SignalSources;

[ApiVersion(EndpointVersion.V1)]
public sealed class ListSignalSourcesEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithoutRequest
    .WithActionResult<IEnumerable<SignalSourceResponse>>
{
    [HttpGet(ApiRoutes.SignalSources.List)]
    public override async Task<ActionResult<IEnumerable<SignalSourceResponse>>> HandleAsync(
        CancellationToken cancellationToken = default) =>
        await ErrorOr<ListSignalSourcesQuery>
            .With(new ListSignalSourcesQuery())
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(sources => sources
                .Select(s => s.ToResponse())
                .ToList())
            .Match(HandleFailure, Ok);
}