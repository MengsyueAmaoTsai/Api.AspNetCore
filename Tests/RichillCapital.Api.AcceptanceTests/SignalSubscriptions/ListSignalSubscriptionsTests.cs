using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.SignalSubscriptions;

namespace RichillCapital.Api.AcceptanceTests.SignalSubscriptions;

public sealed class ListSignalSubscriptionsTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ListSignalSubscriptions()
    {
        var subscriptions = await Client.GetFromJsonAsync<IEnumerable<SignalSubscriptionResponse>>("api/v1/signal-subscriptions");

        subscriptions.Should().NotBeNullOrEmpty();
    }
}
