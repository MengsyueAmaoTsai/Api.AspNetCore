using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.SignalSourceSubscriptions;

namespace RichillCapital.Api.AcceptanceTests.Endpoints.SignalSourceSubscriptions;

public sealed class GetSignalSourceSubscriptionTests(
    AcceptanceTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ReturnSignalSourceSubscription()
    {
        var subscriptionId = "1";

        var subscription = await Client.GetFromJsonAsync<SignalSourceSubscriptionDetailsResponse>(
            $"api/v1/signal-source-subscriptions/{subscriptionId}");

        subscription.Should().NotBeNull();
        subscription!.Id.Should().Be(subscriptionId);
    }
}