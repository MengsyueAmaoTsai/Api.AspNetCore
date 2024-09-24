using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.SignalSources.Commands;

internal sealed class CreateSignalSourceCommandHandler(
    IRepository<SignalSource> _signalSourceRepository,
    IUnitOfWork _unitOfWork) :
    ICommandHandler<CreateSignalSourceCommand, ErrorOr<SignalSourceId>>
{
    public async Task<ErrorOr<SignalSourceId>> Handle(
        CreateSignalSourceCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = SignalSourceId.From(command.Id);

        if (validationResult.IsFailure)
        {
            return ErrorOr<SignalSourceId>.WithError(validationResult.Error);
        }

        var sourceId = validationResult.Value;

        var now = DateTimeOffset.UtcNow;

        var errorOrSignalSource = SignalSource.Create(
            sourceId,
            command.Name,
            command.Description,
            SignalSourceStatus.Draft,
            now);

        if (errorOrSignalSource.HasError)
        {
            return ErrorOr<SignalSourceId>.WithError(errorOrSignalSource.Errors);
        }

        var signalSource = errorOrSignalSource.Value;

        _signalSourceRepository.Add(signalSource);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ErrorOr<SignalSourceId>.With(signalSource.Id);
    }
}