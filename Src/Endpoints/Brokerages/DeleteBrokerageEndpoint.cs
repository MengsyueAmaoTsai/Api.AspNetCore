using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Brokerages.Commands;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Brokerages;

[ApiVersion(EndpointVersion.V1)]
public sealed class DeleteBrokerageEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<string>
    .WithActionResult
{
    [HttpDelete(ApiRoutes.Brokerages.Delete)]
    [SwaggerOperation(Tags = [ApiTags.Brokerages])]
    public override async Task<ActionResult> HandleAsync(
        [FromRoute(Name = nameof(connectionName))] string connectionName,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<string>
            .With(connectionName)
            .Then(name => new DeleteBrokerageCommand
            {
                ConnectionName = name,
            })
            .Then(command => _mediator.Send(command, cancellationToken))
            .Match(HandleFailure, _ => NoContent());
}