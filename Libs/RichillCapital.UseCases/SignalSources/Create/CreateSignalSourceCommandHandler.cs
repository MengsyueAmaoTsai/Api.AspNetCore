
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
        var validationResult = SignalSourceId.From(command.Id);

        if (validationResult.IsFailure)
        {
            return ErrorOr<SignalSourceId>.WithError(validationResult.Error);
        }

        var id = validationResult.Value;
        var now = DateTimeOffset.UtcNow;

        var errorOrSignalSource = SignalSource.Create(
            id,
            command.Name,
            command.Description,
            now);

        if (errorOrSignalSource.HasError)
        {
            return ErrorOr<SignalSourceId>.WithError(errorOrSignalSource.Errors);
        }

        _signalSourceRepository.Add(errorOrSignalSource.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ErrorOr<SignalSourceId>.With(id);
    }
}