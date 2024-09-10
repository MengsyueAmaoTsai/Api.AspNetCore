using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Users;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Users.Queries;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Users;

[ApiVersion(EndpointVersion.V1)]
public sealed class GetUserEndpoint(IMediator _mediator) : AsyncEndpoint
    .WithRequest<GetUserRequest>
    .WithActionResult<UserDetailsResponse>
{
    [HttpGet(ApiRoutes.Users.Get)]
    [SwaggerOperation(Tags = [ApiTags.Users])]
    [Authorize]
    public override async Task<ActionResult<UserDetailsResponse>> HandleAsync(
        [FromRoute] GetUserRequest request,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<GetUserRequest>
            .With(request)
            .Then(req => new GetUserQuery
            {
                UserId = req.UserId,
            })
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(dto => dto.ToDetailsResponse())
            .Match(HandleFailure, Ok);
}