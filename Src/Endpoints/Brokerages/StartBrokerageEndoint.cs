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
    .WithRequest<string>
    .WithActionResult<BrokerageDetailsResponse>
{
    [HttpPost(ApiRoutes.Brokerages.Start)]
    [SwaggerOperation(Tags = [ApiTags.Brokerages])]
    public override async Task<ActionResult<BrokerageDetailsResponse>> HandleAsync(
        [FromRoute(Name = nameof(connectionName))] string connectionName,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<string>
            .With(connectionName)
            .Then(name => new StartBrokerageCommand
            {
                ConnectionName = name,
            })
            .Then(command => _mediator.Send(command, cancellationToken))
            .Then(dto => dto.ToDetailsResponse())
            .Match(HandleFailure, Ok);
}