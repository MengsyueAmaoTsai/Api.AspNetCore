using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Accounts;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Brokerages.Queries;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Brokerages;

[ApiVersion(EndpointVersion.V1)]
public sealed class ListBrokerageAccountsEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<string>
    .WithActionResult<IEnumerable<AccountResponse>>
{
    [HttpGet(ApiRoutes.Brokerages.Accounts.List)]
    [SwaggerOperation(
        Summary = "List all brokerage accounts",
        Description = "List all brokerage accounts",
        Tags = [ApiTags.Brokerages])]
    [AllowAnonymous]
    public override async Task<ActionResult<IEnumerable<AccountResponse>>> HandleAsync(
        [FromRoute(Name = nameof(connectionName))] string connectionName,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<string>
            .With(connectionName)
            .Then(name => new ListBrokerageAccountsQuery
            {
                ConnectionName = name,
            })
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(accounts => accounts
                .Select(a => a.ToResponse())
                .ToList())
            .Match(HandleFailure, Ok);

}