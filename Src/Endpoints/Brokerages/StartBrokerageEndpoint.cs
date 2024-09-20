using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Brokerages;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Brokerages.Commands;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Brokerages;

[ApiVersion(EndpointVersion.V1)]
public sealed class StartBrokerageEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<StartBrokerageRequest>
    .WithActionResult<BrokerageResponse>
{
    [HttpPost(ApiRoutes.Brokerages.Start)]
    [SwaggerOperation(Tags = [ApiTags.Brokerages])]
    public override async Task<ActionResult<BrokerageResponse>> HandleAsync(
        [FromBody] StartBrokerageRequest request,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<StartBrokerageRequest>
            .With(request)
            .Then(req => new StartBrokerageCommand
            {
                Provider = req.Provider,
                Name = req.Name
            })
            .Then(command => _mediator.Send(command, cancellationToken))
            .Then(dto => dto.ToResponse())
            .Match(HandleFailure, Ok);
}

public sealed record StartBrokerageRequest
{
    public required string Provider { get; init; }
    public required string Name { get; init; }
}

