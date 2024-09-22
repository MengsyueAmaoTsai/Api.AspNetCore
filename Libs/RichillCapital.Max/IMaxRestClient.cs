using RichillCapital.Max.Contracts;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Max;

public interface IMaxRestClient
{
    Task<Result<MaxServerTimeResponse>> GetServerTimeAsync(CancellationToken cancellationToken = default);
    Task<Result<MaxMarketResponse[]>> ListMarketsAsync(CancellationToken cancellationToken = default);
    Task<Result<MaxCurrencyResponse[]>> ListCurrenciesAsync(CancellationToken cancellationToken = default);
    Task<Result<MaxAccountBalanceResponse[]>> ListAccountBalancesAsync(string walletType, CancellationToken cancellationToken = default);
    Task<Result<MaxUserInfoResponse>> GetUserInfoAsync(CancellationToken cancellationToken = default);
    Task<Result> SubmitOrderAsync(string walletType, CancellationToken cancellationToken = default);
    Task<Result<MaxOrderResponse[]>> ListOpenOrdersAsync(string walletType, CancellationToken cancellationToken = default);
    Task<Result<MaxOrderResponse[]>> ListClosedOrdersAsync(string walletType, CancellationToken cancellationToken = default);
    Task<Result<MaxOrderResponse[]>> ListOrderHistoryAsync(string walletType, string market, CancellationToken cancellationToken = default);
    Task<Result<MaxCancelAllOrdersResponse[]>> CancelAllOrdersAsync(string walletType, CancellationToken cancellationToken = default);
    Task<Result<MaxCancelOrderResponse>> CancelOrderAsync(CancellationToken cancellationToken = default);
    Task<Result> GetOrderDetailAsync(CancellationToken cancellationToken = default);
}
