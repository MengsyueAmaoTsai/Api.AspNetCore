using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Accounts;
using RichillCapital.UseCases.Accounts.Queries;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Accounts;

[ApiVersion(EndpointVersion.V1)]
public sealed class GetAccountPerformanceEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<string>
    .WithActionResult<AccountPerformanceDto>
{
    [HttpGet(ApiRoutes.Accounts.GetPerformance)]
    [SwaggerOperation(
        Summary = "Retrieves the performance of an account.",
        Description = "Retrieves the performance of an account.",
        Tags = [ApiTags.Accounts])]
    public override async Task<ActionResult<AccountPerformanceDto>> HandleAsync(
        [FromRoute(Name = nameof(accountId))] string accountId,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<string>
            .With(accountId)
            .Then(id => new GetAccountPerformanceQuery
            {
                AccountId = id,
            })
            .Then(query => _mediator.Send(query, cancellationToken))
            .Match(HandleFailure, Ok);
}