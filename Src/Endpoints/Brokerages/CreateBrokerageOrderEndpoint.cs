using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Brokerages;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Brokerages.Commands;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Brokerages;

[ApiVersion(EndpointVersion.V1)]
public sealed class CreateBrokerageOrderEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<CreateBrokerageOrderRequest>
    .WithActionResult<BrokerageOrderCreatedResponse>
{
    [HttpPost(ApiRoutes.Brokerages.Orders.Create)]
    [SwaggerOperation(Tags = [ApiTags.Brokerages])]
    [AllowAnonymous]
    public override async Task<ActionResult<BrokerageOrderCreatedResponse>> HandleAsync(
        [FromRoute] CreateBrokerageOrderRequest request,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<CreateBrokerageOrderRequest>
            .With(request)
            .Then(req => new CreateBrokerageOrderCommand
            {
                ConnectionName = req.ConnectionName,
                Symbol = req.Order.Symbol,
                TradeType = req.Order.TradeType,
                OrderType = req.Order.OrderType,
                TimeInForce = req.Order.TimeInForce,
                Quantity = req.Order.Quantity,
            })
            .Then(command => _mediator.Send(command, cancellationToken))
            .Then(id => new BrokerageOrderCreatedResponse
            {
                Id = id,
            })
            .Match(HandleFailure, Ok);
}