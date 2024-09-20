using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Orders;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Orders.Queries;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Orders;

[ApiVersion(EndpointVersion.V1)]
public sealed class GetOrderEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<string>
    .WithActionResult<OrderDetailsResponse>
{
    [HttpGet(ApiRoutes.Orders.Get)]
    [SwaggerOperation(Tags = [ApiTags.Orders])]
    public override async Task<ActionResult<OrderDetailsResponse>> HandleAsync(
        [FromRoute(Name = "orderId")] string orderId,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<string>
            .With(orderId)
            .Then(id => new GetOrderQuery
            {
                OrderId = id,
            })
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(dto => dto.ToDetailsResponse())
            .Match(HandleFailure, Ok);
}