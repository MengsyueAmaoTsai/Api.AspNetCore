using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Accounts;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Accounts.List;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Accounts;

[ApiVersion(EndpointVersion.V1)]
public sealed class ListAccountsEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithoutRequest
    .WithActionResult<Paged<AccountResponse>>
{
    [HttpGet(ApiRoutes.Accounts.List)]
    [MapToApiVersion(EndpointVersion.V1)]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "List accounts",
        Description = "List all accounts",
        OperationId = "Accounts.List",
        Tags = ["Accounts"])]
    public override async Task<ActionResult<Paged<AccountResponse>>> HandleAsync(
        CancellationToken cancellationToken = default) =>
        await _mediator.Send(new ListAccountsQuery(), cancellationToken)
            .Then(dto => dto.ToPagedResponse())
            .Match(HandleFailure, Ok);
}