using MediatR;

namespace RichillCapital.UseCases.Common;

internal interface ICommand<TResult> :
    IRequest<TResult>;
