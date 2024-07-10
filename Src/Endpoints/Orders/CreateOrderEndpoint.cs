using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Orders;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Orders;

[ApiVersion(EndpointVersion.V1)]
public sealed class CreateOrderEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<CreateOrderRequest>
    .WithActionResult<CreateOrderResponse>
{
    [HttpPost(ApiRoutes.Orders.Create)]
    [MapToApiVersion(EndpointVersion.V1)]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Creates an order.",
        Description = "Creates an order for a given account.",
        OperationId = "Orders.Create",
        Tags = ["Orders"])]
    public override async Task<ActionResult<CreateOrderResponse>> HandleAsync(
        [FromBody] CreateOrderRequest request,
        CancellationToken cancellationToken = default)
    {
        return Ok(new CreateOrderResponse
        {
            OrderId = Guid.NewGuid().ToString(),
        });
    }
}