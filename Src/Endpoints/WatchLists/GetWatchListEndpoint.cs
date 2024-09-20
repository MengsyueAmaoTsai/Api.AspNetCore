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
public sealed class GetWatchListEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<string>
    .WithActionResult<WatchListDetailsResponse>
{
    [HttpGet(ApiRoutes.WatchLists.Get)]
    [SwaggerOperation(Tags = [ApiTags.WatchLists])]
    public override async Task<ActionResult<WatchListDetailsResponse>> HandleAsync(
        [FromRoute(Name = "watchListId")] string watchListId,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<string>
            .With(watchListId)
            .Then(id => new GetWatchListQuery
            {
                WatchListId = id,
            })
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(dto => dto.ToDetailsResponse())
            .Match(HandleFailure, Ok);
}