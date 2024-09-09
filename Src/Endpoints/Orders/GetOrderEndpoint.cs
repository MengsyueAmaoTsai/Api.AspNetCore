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
public sealed class GetOrderEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<GetOrderRequest>
    .WithActionResult<OrderDetailsResponse>
{
    [HttpGet(ApiRoutes.Orders.Get)]
    [SwaggerOperation(Tags = [ApiTags.Orders])]
    public override async Task<ActionResult<OrderDetailsResponse>> HandleAsync(
        [FromRoute] GetOrderRequest request,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<GetOrderRequest>
            .With(request)
            .Then(req => new GetOrderQuery
            {
                OrderId = req.OrderId,
            })
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(dto => dto.ToDetailsResponse())
            .Match(HandleFailure, Ok);
}