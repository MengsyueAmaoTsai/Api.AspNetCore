using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Brokerages;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Brokerages.Queries;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Brokerages;

[ApiVersion(EndpointVersion.V1)]
public sealed class ListBrokeragesEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithoutRequest
    .WithActionResult<IEnumerable<BrokerageResponse>>
{
    [HttpGet(ApiRoutes.Brokerages.List)]
    [SwaggerOperation(Tags = [ApiTags.Brokerages])]
    public override async Task<ActionResult<IEnumerable<BrokerageResponse>>> HandleAsync(
        CancellationToken cancellationToken = default) =>
        await ErrorOr<ListBrokeragesQuery>
            .With(new ListBrokeragesQuery())
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(brokerages => brokerages
                .Select(b => b.ToResponse())
                .ToList())
            .Match(HandleFailure, Ok);
}