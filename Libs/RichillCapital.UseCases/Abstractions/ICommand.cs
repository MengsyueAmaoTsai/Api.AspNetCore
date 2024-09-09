using MediatR;

namespace RichillCapital.UseCases.Abstractions;

internal interface ICommand<TResult> :
    IRequest<TResult>;
