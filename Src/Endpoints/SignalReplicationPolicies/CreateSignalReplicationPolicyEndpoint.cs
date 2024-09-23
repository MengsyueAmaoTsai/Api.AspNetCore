using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Api.Endpoints;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.SignalReplicationPolicies.Commands;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Contracts.SignalReplicationPolicies;

[ApiVersion(EndpointVersion.V1)]
public sealed class CreateSignalReplicationPolicyEndpoint(IMediator _mediator) : AsyncEndpoint
    .WithRequest<CreateSignalReplicationPolicyRequest>
    .WithActionResult<SignalReplicationPolicyCreatedResponse>
{
    [HttpPost(ApiRoutes.SignalReplicationPolicies.Create)]
    [SwaggerOperation(
        Tags = [ApiTags.SignalReplicationPolicies],
        Summary = "Create a new signal replication policy.",
        Description = "Creates a new signal replication policy.")]
    public override async Task<ActionResult<SignalReplicationPolicyCreatedResponse>> HandleAsync(
        [FromBody] CreateSignalReplicationPolicyRequest request,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<CreateSignalReplicationPolicyRequest>
            .With(request)
            .Then(req => new CreateSignalReplicationPolicyCommand
            {
                UserId = req.UserId,
                SourceId = req.SourceId,
                Multiplier = req.Multiplier
            })
            .Then(command => _mediator.Send(command, cancellationToken))
            .Then(id => new SignalReplicationPolicyCreatedResponse
            {
                Id = id.Value
            })
            .Match(HandleFailure, Ok);
}