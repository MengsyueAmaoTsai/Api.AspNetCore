using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Api.Endpoints;
using RichillCapital.Contracts;
using RichillCapital.Contracts.Instruments;
using RichillCapital.SharedKernel.Monads;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.UseCases.DataFeeds.Queries;

[ApiVersion(EndpointVersion.V1)]
public sealed class ListDataFeedInstrumentsEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<string>
    .WithActionResult<IEnumerable<InstrumentResponse>>
{
    [HttpGet(ApiRoutes.DataFeeds.Instruments.List)]
    [SwaggerOperation(
        Summary = "List all instruments for a data feed.",
        Description = "List all instruments for a data feed.",
        Tags = [ApiTags.DataFeeds])]
    public override async Task<ActionResult<IEnumerable<InstrumentResponse>>> HandleAsync(
        [FromRoute(Name = nameof(connectionName))] string connectionName,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<string>
            .With(connectionName)
            .Then(name => new ListDataFeedInstrumentsQuery
            {
                ConnectionName = name,
            })
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(instruments => instruments.
                Select(ins => ins.ToResponse())
                .ToList())
            .Match(HandleFailure, Ok);
}