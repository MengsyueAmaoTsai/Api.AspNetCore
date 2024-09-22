using RichillCapital.Max.Contracts;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Max;

public interface IMaxRestClient
{
    Task<Result<MaxServerTimeResponse>> GetServerTimeAsync(CancellationToken cancellationToken = default);
    Task<Result<MaxMarketResponse[]>> ListMarketsAsync(CancellationToken cancellationToken = default);
    Task<Result<MaxCurrencyResponse[]>> ListCurrenciesAsync(CancellationToken cancellationToken = default);
    Task<Result<MaxAccountBalanceResponse[]>> ListAccountBalancesAsync(string pathWalletType, CancellationToken cancellationToken = default);
    Task<Result<MaxUserInfoResponse>> GetUserInfoAsync(CancellationToken cancellationToken = default);
    Task<Result> SubmitOrderAsync(string pathWalletType, CancellationToken cancellationToken = default);
}
