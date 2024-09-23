using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Api.AcceptanceTests;
using RichillCapital.Contracts.SignalSubscriptions;

public sealed class GetSignalSubscriptionTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ReturnSignalSubscription()
    {
        var id = "1";
        var subscription = await Client.GetFromJsonAsync<SignalSubscriptionDetailsResponse>($"api/v1/signal-subscriptions/{id}");

        subscription.Should().NotBeNull();
        subscription!.Id.Should().Be(id);
    }
}