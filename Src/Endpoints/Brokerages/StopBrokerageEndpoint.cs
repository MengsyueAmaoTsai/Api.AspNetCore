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
public sealed class StopBrokerageEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<StopBrokerageRequest>
    .WithActionResult<BrokerageResponse>
{
    [HttpPost(ApiRoutes.Brokerages.Stop)]
    [SwaggerOperation(Tags = [ApiTags.Brokerages])]
    public override async Task<ActionResult<BrokerageResponse>> HandleAsync(
        [FromBody] StopBrokerageRequest request,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<StopBrokerageRequest>
            .With(request)
            .Then(req => new StopBrokerageCommand
            {
                ConnectionName = req.ConnectionName
            })
            .Then(command => _mediator.Send(command, cancellationToken))
            .Then(dto => dto.ToResponse())
            .Match(HandleFailure, Ok);
}