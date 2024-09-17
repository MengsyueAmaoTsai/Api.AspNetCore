using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Abstractions;

public interface IOrderPlacementEvaluator
{
    Task<Result> EvaluateAsync(
        Order order,
        CancellationToken cancellationToken = default);
}