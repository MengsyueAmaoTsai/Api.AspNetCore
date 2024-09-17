using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
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
    .WithRequest<GetAccountRequest>
    .WithActionResult<AccountDetailsResponse>
{
    [HttpGet(ApiRoutes.Accounts.Get)]
    [SwaggerOperation(Tags = [ApiTags.Accounts])]
    [AllowAnonymous]
    public override async Task<ActionResult<AccountDetailsResponse>> HandleAsync(
        [FromRoute] GetAccountRequest request,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<GetAccountRequest>
            .With(request)
            .Then(req => new GetAccountQuery
            {
                AccountId = req.AccountId,
            })
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(account => account.ToDetailsResponse())
            .Match(HandleFailure, Ok);
}