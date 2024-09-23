using FluentAssertions;

using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.UnitTests;

public sealed class SymbolTests
{
    [Theory]
    [InlineData("")]
    [InlineData("00000000000000000000000000000000000000000000000000000000000000000000")]
    [InlineData("BINANCEBTCUSDT")]
    [InlineData("BINANCE:BTCUSDT/")]
    public void From_When_ValueIsInvalid_Should_ReturnFailureResult(
        string invalidValue)
    {
        var symbolResult = Symbol.From(invalidValue);

        symbolResult.IsFailure.Should().BeTrue();

        var error = symbolResult.Error;

        error.Code.Should().Be(ErrorType.Validation.ToString());
    }

    [Theory]
    [InlineData("BINANCE:BTCUSDT")]
    [InlineData("BINANCE:BTCUSDT.P")]
    [InlineData("TAIFEX:TXF!1")]
    public void From_When_ValueIsValid_Should_ReturnSuccessResult(string value)
    {
        var symbolResult = Symbol.From(value);

        symbolResult.IsSuccess.Should().BeTrue(symbolResult.Error.Message);

        var symbol = symbolResult.Value;
        symbol.Value.Should().Be(value);
    }
}
