using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.BackTestReports;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.BackTestReports;

namespace RichillCapital.Api.Endpoints.BackTestReports;

[ApiVersion(EndpointVersion.V1)]
public sealed class GenerateBackTestReportForTradingViewEndpoint(
    ILogger<GenerateBackTestReportForTradingViewEndpoint> _logger,
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<GenerateBackTestReportForTradingViewRequest>
    .WithActionResult<BackTestReportResponse>
{
    [HttpPost(ApiRoutes.BackTestReports.GenerateTradingView)]
    [Consumes("multipart/form-data")]
    public override async Task<ActionResult<BackTestReportResponse>> HandleAsync(
        [FromForm] GenerateBackTestReportForTradingViewRequest request,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<GenerateBackTestReportForTradingViewRequest>
            .With(request)
            .Then(req => new GenerateBackTestReportForTradingViewCommand
            {
                FileName = req.ListOfTradeFile.FileName,
                ContentType = req.ListOfTradeFile.ContentType,
                Length = req.ListOfTradeFile.Length,
                FileStream = req.ListOfTradeFile.OpenReadStream(),
            })
            .Then(command => _mediator.Send(command, cancellationToken))
            .Then(dto => dto.ToResponse())
            .Match(HandleFailure, Ok);
}