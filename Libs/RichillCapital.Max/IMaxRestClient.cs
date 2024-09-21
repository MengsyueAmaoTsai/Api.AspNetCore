using RichillCapital.Max.Contracts;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Max;

public interface IMaxRestClient
{
    Task<Result<MaxServerTimeResponse>> GetServerTimeAsync(CancellationToken cancellationToken = default);
    Task<Result> SubmitOrderAsync(CancellationToken cancellationToken = default);
}
