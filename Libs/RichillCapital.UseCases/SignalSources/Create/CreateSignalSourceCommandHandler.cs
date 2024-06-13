using RichillCapital.Domain;
using RichillCapital.Domain.Common.Repositories;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.SignalSources.Create;

internal sealed class CreateSignalSourceCommandHandler(
    IRepository<SignalSource> _signalSourceRepository,
    IUnitOfWork _unitOfWork) :
    ICommandHandler<CreateSignalSourceCommand, ErrorOr<SignalSourceId>>
{
    public async Task<ErrorOr<SignalSourceId>> Handle(
        CreateSignalSourceCommand command,
        CancellationToken cancellationToken)
    {
        var sourceIdResult = SignalSourceId.From(command.Id);

        if (sourceIdResult.IsFailure)
        {
            return sourceIdResult.Error
                .ToErrorOr<SignalSourceId>();
        }

        var sourceId = sourceIdResult.Value;

        var errorOrSignalSource = SignalSource.Create(
            sourceId,
            command.Name,
            command.Description);

        if (errorOrSignalSource.HasError)
        {
            return errorOrSignalSource.Errors.First()
                .ToErrorOr<SignalSourceId>();
        }

        var signalSource = errorOrSignalSource.Value;

        _signalSourceRepository.Add(signalSource);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return signalSource.Id
            .ToErrorOr();
    }
}