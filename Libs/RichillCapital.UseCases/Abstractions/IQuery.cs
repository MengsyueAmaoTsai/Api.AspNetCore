using MediatR;

namespace RichillCapital.UseCases.Abstractions;

internal interface IQuery<TResult> :
    IRequest<TResult>;
