using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Accounts;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Accounts.Commands;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Accounts;

[ApiVersion(EndpointVersion.V1)]
public sealed class CreateAccountEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<CreateAccountRequest>
    .WithActionResult<AccountCreatedResponse>
{
    [HttpPost(ApiRoutes.Accounts.Create)]
    [SwaggerOperation(
        Summary = "Create an account.",
        Description = "Create an account for a user.",
        Tags = [ApiTags.Accounts])]
    public override async Task<ActionResult<AccountCreatedResponse>> HandleAsync(
        [FromBody] CreateAccountRequest request,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<CreateAccountRequest>
            .With(request)
            .Then(req => new CreateAccountCommand
            {
                UserId = req.UserId,
                ConnectionName = req.ConnectionName,
                Alias = req.Alias,
                Currency = req.Currency,
            })
            .Then(command => _mediator.Send(command, cancellationToken))
            .Then(id => new AccountCreatedResponse
            {
                Id = id.Value,
            })
            .Match(HandleFailure, Ok);
}