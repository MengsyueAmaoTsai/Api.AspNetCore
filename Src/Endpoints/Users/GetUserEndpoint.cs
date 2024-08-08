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
public sealed class GetUserEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<string>
    .WithActionResult<UserDetailsResponse>
{
    [HttpGet(ApiRoutes.Users.Get)]
    [MapToApiVersion(EndpointVersion.V1)]
    [SwaggerOperation(Tags = ["Users"])]
    public override async Task<ActionResult<UserDetailsResponse>> HandleAsync(
        [FromRoute(Name = "userId")] string request,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<string>.With(request)
            .Then(id => new GetUserQuery
            {
                UserId = id
            })
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(dto => dto.ToDetailsResponse())
            .Match(HandleFailure, Ok);
}