using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Users;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Users.Queries;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Users;

[ApiVersion(EndpointVersion.V1)]
public sealed class ListUsersEndpoint(IMediator _mediator) :
    AsyncEndpoint
    .WithoutRequest
    .WithActionResult<IEnumerable<UserResponse>>
{
    [HttpGet(ApiRoutes.Users.List)]
    [SwaggerOperation(Tags = [ApiTags.Users])]
    public override async Task<ActionResult<IEnumerable<UserResponse>>> HandleAsync(
        CancellationToken cancellationToken = default) =>
        await ErrorOr<ListUsersQuery>
            .With(new ListUsersQuery())
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(users => users.Select(u => u.ToResponse()))
            .Match(HandleFailure, Ok);
}