using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.WatchLists;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.WatchLists.Commands;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.WatchLists;

[ApiVersion(EndpointVersion.V1)]
public sealed class UpdateWatchListEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<UpdateWatchListRequest>
    .WithActionResult<WatchListDetailsResponse>
{
    [HttpPut(ApiRoutes.WatchLists.Update)]
    [SwaggerOperation(Tags = [ApiTags.WatchLists])]
    public override async Task<ActionResult<WatchListDetailsResponse>> HandleAsync(
        [FromRoute] UpdateWatchListRequest request,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<UpdateWatchListRequest>
            .With(request)
            .Then(req => new UpdateWatchListCommand
            {
                WatchListId = req.WatchListId,
                Name = req.Body.Name,
            })
            .Then(command => _mediator.Send(command, cancellationToken))
            .Then(dto => dto.ToResponse())
            .Match(HandleFailure, Ok);
}