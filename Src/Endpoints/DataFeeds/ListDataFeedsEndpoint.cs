using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.DataFeeds;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.DataFeeds.Queries;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.DataFeeds;

[ApiVersion(EndpointVersion.V1)]
public sealed class ListDataFeedsEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithoutRequest
    .WithActionResult<IEnumerable<DataFeedResponse>>
{
    [HttpGet(ApiRoutes.DataFeeds.List)]
    [SwaggerOperation(
        Summary = "List all data feeds",
        Description = "List all data feeds",
        Tags = [ApiTags.DataFeeds])]
    public override async Task<ActionResult<IEnumerable<DataFeedResponse>>> HandleAsync(
        CancellationToken cancellationToken = default) =>
        await ErrorOr<ListDataFeedsQuery>
            .With(new ListDataFeedsQuery())
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(dataFeeds => dataFeeds
                .Select(dto => dto.ToResponse())
                .ToList())
            .Match(HandleFailure, Ok);
}