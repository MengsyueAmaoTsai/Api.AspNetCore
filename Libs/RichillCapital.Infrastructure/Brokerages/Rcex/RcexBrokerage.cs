using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Brokerages;

namespace RichillCapital.Infrastructure.Brokerages.Rcex;

internal sealed class RcexBrokerage(
    ILogger<RcexBrokerage> _logger,
    Guid id,
    string name) :
    Brokerage(_logger, id, name)
{
}