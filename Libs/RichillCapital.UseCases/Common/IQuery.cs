using MediatR;

namespace RichillCapital.UseCases.Common;

internal interface IQuery<TResult> :
    IRequest<TResult>;
