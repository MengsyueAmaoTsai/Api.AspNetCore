using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Api.Endpoints;
using RichillCapital.Contracts;
using RichillCapital.Contracts.Orders;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Brokerages.Queries;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.UseCases.Brokerages.Endpoints;

[ApiVersion(EndpointVersion.V1)]
public sealed class ListBrokerageOrdersEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<string>
    .WithActionResult<IEnumerable<OrderResponse>>
{
    [HttpGet(ApiRoutes.Brokerages.Orders.List)]
    [SwaggerOperation(
        Summary = "List brokerage orders",
        Description = "List brokerage orders",
        Tags = ["Brokerages"])]
    public override async Task<ActionResult<IEnumerable<OrderResponse>>> HandleAsync(
        [FromRoute(Name = nameof(connectionName))] string connectionName,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<string>
            .With(connectionName)
            .Then(name => new ListBrokerageOrdersQuery
            {
                ConnectionName = name,
            })
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(orders => orders
                .Select(o => o.ToResponse())
                .ToList())
            .Match(HandleFailure, Ok);
}