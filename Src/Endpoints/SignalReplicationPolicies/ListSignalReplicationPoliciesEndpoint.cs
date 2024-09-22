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
public sealed class ListSignalReplicationPoliciesEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithoutRequest
    .WithActionResult<IEnumerable<SignalReplicationPolicyResponse>>
{
    [HttpGet(ApiRoutes.SignalReplicationPolicies.List)]
    [SwaggerOperation(
        Summary = "List signal replication policies",
        Description = "List signal replication policies",
        Tags = [ApiTags.SignalReplicationPolicies])]
    public override async Task<ActionResult<IEnumerable<SignalReplicationPolicyResponse>>> HandleAsync(
        CancellationToken cancellationToken = default) =>
        await ErrorOr<ListSignalReplicationPoliciesQuery>
            .With(new ListSignalReplicationPoliciesQuery())
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(policies => policies
                .Select(p => p.ToResponse())
                .ToList())
            .Match(HandleFailure, Ok);
}