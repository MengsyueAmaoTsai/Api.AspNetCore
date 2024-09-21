namespace RichillCapital.Api.AcceptanceTests.Brokerages;

public abstract class BrokerageTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    protected const string ExistingConnectionName = "RichillCapital.Binance";
}