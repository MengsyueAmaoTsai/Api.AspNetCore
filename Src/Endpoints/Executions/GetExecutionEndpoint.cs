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
public sealed class GetExecutionEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<string>
    .WithActionResult<ExecutionDetailsResponse>
{
    [HttpGet(ApiRoutes.Executions.Get)]
    [SwaggerOperation(Tags = [ApiTags.Executions])]
    public override async Task<ActionResult<ExecutionDetailsResponse>> HandleAsync(
        [FromRoute(Name = "executionId")] string executionId,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<string>
            .With(executionId)
            .Then(id => new GetExecutionQuery
            {
                ExecutionId = id,
            })
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(dto => dto.ToDetailsResponse())
            .Match(HandleFailure, Ok);
}