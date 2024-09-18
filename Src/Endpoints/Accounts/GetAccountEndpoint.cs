using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Accounts;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Accounts.Queries;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Accounts;

[ApiVersion(EndpointVersion.V1)]
public sealed class GetAccountEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<string>
    .WithActionResult<AccountDetailsResponse>
{
    [HttpGet(ApiRoutes.Accounts.Get)]
    [SwaggerOperation(Tags = [ApiTags.Accounts])]
    public override async Task<ActionResult<AccountDetailsResponse>> HandleAsync(
        [FromRoute(Name = "accountId")] string accountId,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<string>
            .With(accountId)
            .Then(id => new GetAccountQuery
            {
                AccountId = id,
            })
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(account => account.ToDetailsResponse())
            .Match(HandleFailure, Ok);
}