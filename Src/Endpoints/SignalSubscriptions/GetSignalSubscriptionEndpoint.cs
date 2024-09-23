using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.SignalSubscriptions;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.SignalSubscriptions.Queries;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.SignalSubscriptions;

[ApiVersion(EndpointVersion.V1)]
public sealed class GetSignalSubscriptionEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<string>
    .WithActionResult<SignalSubscriptionDetailsResponse>
{
    [HttpGet(ApiRoutes.SignalSubscriptions.Get)]
    [SwaggerOperation(
        Summary = "Get signal subscription",
        Description = "Get a signal subscription by id",
        Tags = [ApiTags.SignalSubscriptions])]
    [AllowAnonymous]
    public override async Task<ActionResult<SignalSubscriptionDetailsResponse>> HandleAsync(
        [FromRoute(Name = nameof(signalSubscriptionId))] string signalSubscriptionId,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<string>
            .With(signalSubscriptionId)
            .Then(id => new GetSignalSubscriptionQuery
            {
                SignalSubscriptionId = id,
            })
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(subscription => subscription.ToDetailsResponse())
            .Match(HandleFailure, Ok);
}