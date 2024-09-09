using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Api.Endpoints;
using RichillCapital.Contracts;
using RichillCapital.Contracts.Orders;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Orders.Queries;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Endpoints.Orders;

[ApiVersion(EndpointVersion.V1)]
public sealed class ListOrdersEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithoutRequest
    .WithActionResult<IEnumerable<OrderResponse>>
{
    [HttpGet(ApiRoutes.Orders.List)]
    [SwaggerOperation(Tags = [ApiTags.Orders])]
    public override async Task<ActionResult<IEnumerable<OrderResponse>>> HandleAsync(
        CancellationToken cancellationToken = default) =>
        await ErrorOr<ListOrdersQuery>
            .With(new ListOrdersQuery())
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(orders => orders
                .Select(o => o.ToResponse())
                .ToList())
            .Match(HandleFailure, Ok);
}
