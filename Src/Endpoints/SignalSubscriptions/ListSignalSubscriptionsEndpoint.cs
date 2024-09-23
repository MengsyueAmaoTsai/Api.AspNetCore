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
public sealed class ListSignalSubscriptionsEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithoutRequest
    .WithActionResult<IEnumerable<SignalSubscriptionResponse>>
{
    [HttpGet(ApiRoutes.SignalSubscriptions.List)]
    [SwaggerOperation(
        Summary = "List signal subscriptions",
        Description = "List all signal subscriptions",
        Tags = [ApiTags.SignalSubscriptions])]
    [AllowAnonymous]
    public override async Task<ActionResult<IEnumerable<SignalSubscriptionResponse>>> HandleAsync(
        CancellationToken cancellationToken = default) =>
        await ErrorOr<ListSignalSubscriptionsQuery>
            .With(new ListSignalSubscriptionsQuery())
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(subscriptions => subscriptions
                .Select(s => s.ToResponse())
                .ToList())
            .Match(HandleFailure, Ok);
}
