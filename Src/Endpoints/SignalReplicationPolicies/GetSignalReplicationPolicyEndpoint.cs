using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.SignalReplicationPolicies;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.SignalReplicationPolicies.Queries;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.SignalReplicationPolicies;

[ApiVersion(EndpointVersion.V1)]
public sealed class GetSignalReplicationPolicyEndpoint(IMediator _mediator) : AsyncEndpoint
    .WithRequest<string>
    .WithActionResult<SignalReplicationPolicyDetailsResponse>
{
    [HttpGet(ApiRoutes.SignalReplicationPolicies.Get)]
    [SwaggerOperation(
        Summary = "Get signal replication policy",
        Description = "Get signal replication policy",
        Tags = [ApiTags.SignalReplicationPolicies])]
    public override async Task<ActionResult<SignalReplicationPolicyDetailsResponse>> HandleAsync(
        [FromRoute(Name = nameof(signalReplicationPolicyId))] string signalReplicationPolicyId,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<string>
            .With(signalReplicationPolicyId)
            .Then(id => new GetSignalReplicationPolicyQuery
            {
                SignalReplicationPolicyId = id,
            })
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(dto => dto.ToDetailsResponse())
            .Match(HandleFailure, Ok);

}