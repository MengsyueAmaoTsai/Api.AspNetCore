using RichillCapital.Domain.Abstractions;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

internal sealed class OrderPlacementEvaluator :
    IOrderPlacementEvaluator
{
    public async Task<Result> EvaluateAsync(
        Order order,
        CancellationToken cancellationToken = default)
    {
        return Result.Success;
    }
}