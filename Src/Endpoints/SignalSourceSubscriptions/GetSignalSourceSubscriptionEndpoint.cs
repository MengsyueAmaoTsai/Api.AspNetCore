using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.SignalSourceSubscriptions;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.SignalSourceSubscriptions;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.SignalSourceSubscriptions;

[ApiVersion(EndpointVersion.V1)]
public sealed class GetSignalSourceSubscriptionEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<string>
    .WithActionResult<SignalSourceSubscriptionDetailsResponse>
{
    [HttpGet(ApiRoutes.SignalSourceSubscriptions.Get)]
    [MapToApiVersion(EndpointVersion.V1)]
    [SwaggerOperation(Tags = ["SignalSourceSubscriptions"])]
    public override async Task<ActionResult<SignalSourceSubscriptionDetailsResponse>> HandleAsync(
        [FromRoute(Name = "signalSourceSubscriptionId")] string request,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<string>.With(request)
            .Then(id => new GetSignalSourceSubscriptionQuery { SignalSourceSubscriptionId = id })
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(dto => dto.ToDetailsResponse())
            .Match(HandleFailure, Ok);
}