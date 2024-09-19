using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Api.Endpoints;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.WatchLists.Commands;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Contracts.WatchLists;

[ApiVersion(EndpointVersion.V1)]
public sealed class CreateWatchListEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<CreateWatchListRequest>
    .WithActionResult<WatchListCreatedResponse>
{
    [HttpPost(ApiRoutes.WatchLists.Create)]
    [SwaggerOperation(Tags = [ApiTags.WatchLists])]
    public override async Task<ActionResult<WatchListCreatedResponse>> HandleAsync(
        CreateWatchListRequest request,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<CreateWatchListRequest>
            .With(request)
            .Then(req => new CreateWatchListCommand
            {
                UserId = req.UserId,
                Name = req.Name,
            })
            .Then(command => _mediator.Send(command, cancellationToken))
            .Then(id => new WatchListCreatedResponse { Id = id.Value })
            .Match(HandleFailure, Ok);
}