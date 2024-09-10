using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.SignalSources;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.SignalSources.Commands;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.SignalSources;

[ApiVersion(EndpointVersion.V1)]
public sealed class CreateSignalSourceEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<CreateSignalSourceRequest>
    .WithActionResult<SignalSourceCreatedResponse>
{
    [HttpPost(ApiRoutes.SignalSources.Create)]
    [SwaggerOperation(Tags = [ApiTags.SignalSources])]
    [Authorize]
    public override async Task<ActionResult<SignalSourceCreatedResponse>> HandleAsync(
        [FromBody] CreateSignalSourceRequest request,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<CreateSignalSourceRequest>
            .With(request)
            .Then(req => new CreateSignalSourceCommand
            {
                Id = req.Id,
                Name = req.Name,
                Description = req.Description,
            })
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(id => new SignalSourceCreatedResponse
            {
                Id = id.Value
            })
            .Match(HandleFailure, Ok);
}