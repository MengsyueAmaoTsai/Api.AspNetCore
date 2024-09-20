using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.WatchLists.Commands;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.WatchLists;

[ApiVersion(EndpointVersion.V1)]
public sealed class DeleteWatchListEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<string>
    .WithActionResult
{
    [HttpDelete(ApiRoutes.WatchLists.Delete)]
    [SwaggerOperation(Tags = [ApiTags.WatchLists])]
    public override async Task<ActionResult> HandleAsync(
        [FromRoute(Name = "watchListId")] string watchListId,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<string>
            .With(watchListId)
            .Then(id => new DeleteWatchListCommand
            {
                WatchListId = id,
            })
            .Then(command => _mediator.Send(command, cancellationToken))
            .Match(HandleFailure, _ => NoContent());
}