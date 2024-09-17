using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Trades;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Trades.Queries;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Trades;

[ApiVersion(EndpointVersion.V1)]
public sealed class ListTradesEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithoutRequest
    .WithActionResult<IEnumerable<TradeResponse>>
{
    [HttpGet(ApiRoutes.Trades.List)]
    [SwaggerOperation(Tags = [ApiTags.Trades])]
    public override async Task<ActionResult<IEnumerable<TradeResponse>>> HandleAsync(
        CancellationToken cancellationToken = default) =>
        await ErrorOr<ListTradesQuery>
            .With(new ListTradesQuery())
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(trades => trades
                .Select(t => t.ToResponse())
                .ToList())
            .Match(HandleFailure, Ok);
}