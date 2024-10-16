using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Brokerages;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Brokerages.Queries;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Brokerages;

[ApiVersion(EndpointVersion.V1)]
public sealed class GetBrokerageEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<string>
    .WithActionResult<BrokerageDetailsResponse>
{
    [HttpGet(ApiRoutes.Brokerages.Get)]
    [SwaggerOperation(
        Summary = "Get a brokerage by connection name.",
        OperationId = "Brokerages.Get",
        Tags = [ApiTags.Brokerages])]
    [AllowAnonymous]
    public override async Task<ActionResult<BrokerageDetailsResponse>> HandleAsync(
        [FromRoute(Name = nameof(connectionName))] string connectionName,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<string>
            .With(connectionName)
            .Then(name => new GetBrokerageQuery
            {
                ConnectionName = name,
            })
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(dto => dto.ToDetailsResponse())
            .Match(HandleFailure, Ok);
}