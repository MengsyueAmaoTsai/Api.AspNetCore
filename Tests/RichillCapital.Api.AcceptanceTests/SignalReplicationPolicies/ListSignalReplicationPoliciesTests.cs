using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.SignalReplicationPolicies;

namespace RichillCapital.Api.AcceptanceTests.SignalReplicationPolicies;

public sealed class ListSignalReplicationPoliciesTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ListSignalReplicationPolicies()
    {
        var policies = await Client.GetFromJsonAsync<IEnumerable<SignalReplicationPolicyResponse>>("api/v1/signal-replication-policies");

        policies.Should().NotBeNull();
    }
}