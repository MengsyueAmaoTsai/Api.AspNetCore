using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.DataFeeds;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.DataFeeds.Queries;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.DataFeeds;

[ApiVersion(EndpointVersion.V1)]
public sealed class GetDataFeedEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<string>
    .WithActionResult<DataFeedDetailsResponse>
{
    [HttpGet(ApiRoutes.DataFeeds.Get)]
    [SwaggerOperation(
        Summary = "Get a data feed by connection name.",
        OperationId = "DataFeeds.Get",
        Tags = [ApiTags.DataFeeds])]
    [AllowAnonymous]
    public override async Task<ActionResult<DataFeedDetailsResponse>> HandleAsync(
        [FromRoute(Name = nameof(connectionName))] string connectionName,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<string>
            .With(connectionName)
            .Then(name => new GetDataFeedQuery

            {
                ConnectionName = name
            })
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(dto => dto.ToDetailsResponse())
            .Match(HandleFailure, Ok);
}