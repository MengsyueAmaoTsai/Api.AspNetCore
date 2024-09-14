using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Executions;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Executions.Queries;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Executions;

[ApiVersion(EndpointVersion.V1)]
public sealed class ListExecutionsEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithoutRequest
    .WithActionResult<IEnumerable<ExecutionResponse>>
{
    [HttpGet(ApiRoutes.Executions.List)]
    [SwaggerOperation(Tags = [ApiTags.Executions])]
    public override async Task<ActionResult<IEnumerable<ExecutionResponse>>> HandleAsync(
        CancellationToken cancellationToken = default) =>
        await ErrorOr<ListExecutionsQuery>
            .With(new ListExecutionsQuery())
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(executions => executions
                .Select(e => e.ToResponse())
                .ToList())
            .Match(HandleFailure, Ok);
}