using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.WatchLists;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.WatchLists.Queries;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.WatchLists;

[ApiVersion(EndpointVersion.V1)]
public sealed class ListWatchListsEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithoutRequest
    .WithActionResult<IEnumerable<WatchListResponse>>
{
    [HttpGet(ApiRoutes.WatchLists.List)]
    [SwaggerOperation(Tags = [ApiTags.WatchLists])]
    public override async Task<ActionResult<IEnumerable<WatchListResponse>>> HandleAsync(
        CancellationToken cancellationToken = default) =>
        await ErrorOr<ListWatchListsQuery>
            .With(new ListWatchListsQuery())
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(lists => lists
                .Select(list => list.ToResponse())
                .ToList())
            .Match(HandleFailure, Ok);
}