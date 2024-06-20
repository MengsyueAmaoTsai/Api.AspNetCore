using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Users;
using RichillCapital.SharedKernel.Monads;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Users;

[ApiVersion(EndpointVersion.V1)]
public sealed class GetUserEndpoint(IMediator _mediator) : AsyncEndpoint
    .WithRequest<GetUserByIdRequest>
    .WithActionResult<UserDetailsResponse>
{
    [HttpGet(ApiRoutes.Users.Get)]
    [MapToApiVersion(EndpointVersion.V1)]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Get user",
        Description = "Get user by id",
        OperationId = "Users.Get",
        Tags = ["Users"])]
    public override async Task<ActionResult<UserDetailsResponse>> HandleAsync(
        [FromRoute] GetUserByIdRequest request,
        CancellationToken cancellationToken = default) =>
        await request
            .ToErrorOr()
            .Then(req => req.ToQuery())
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(dto => dto.ToDetailsResponse())
            .Match(HandleFailure, Ok);
}
