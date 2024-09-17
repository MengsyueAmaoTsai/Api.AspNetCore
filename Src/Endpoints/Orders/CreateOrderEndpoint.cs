using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Orders;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Orders.Commands;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Orders;

[ApiVersion(EndpointVersion.V1)]
public sealed class CreateOrderEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<CreateOrderRequest>
    .WithActionResult<OrderCreatedResponse>
{
    [HttpPost(ApiRoutes.Orders.Create)]
    [SwaggerOperation(Tags = [ApiTags.Orders])]
    public override async Task<ActionResult<OrderCreatedResponse>> HandleAsync(
        [FromBody] CreateOrderRequest request,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<CreateOrderRequest>
            .With(request)
            .Then(req => new CreateOrderCommand
            {
                AccountId = req.AccountId,
                Symbol = req.Symbol,
                TradeType = req.TradeType,
                OrderType = req.OrderType,
                TimeInForce = req.TimeInForce,
                Quantity = req.Quantity
            })
            .Then(command => _mediator.Send(command, cancellationToken))
            .Then(id => new OrderCreatedResponse { Id = id.Value })
            .Match(HandleFailure, Ok);
}