using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Brokerages;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Brokerages.Commands;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Brokerages;

[ApiVersion(EndpointVersion.V1)]
public sealed class CreateBrokerageEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<CreateBrokerageRequest>
    .WithActionResult<BrokerageDetailsResponse>
{
    [HttpPost(ApiRoutes.Brokerages.Create)]
    [SwaggerOperation(Tags = [ApiTags.Brokerages])]
    public override async Task<ActionResult<BrokerageDetailsResponse>> HandleAsync(
        [FromBody] CreateBrokerageRequest request,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<CreateBrokerageRequest>
            .With(request)
            .Then(req => new CreateBrokerageCommand
            {
                Provider = req.Provider,
                Name = req.Name,
            })
            .Then(command => _mediator.Send(command, cancellationToken))
            .Then(dto => dto.ToDetailsResponse())
            .Match(HandleFailure, Ok);
}