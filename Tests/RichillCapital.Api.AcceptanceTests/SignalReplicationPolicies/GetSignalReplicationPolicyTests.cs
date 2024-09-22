using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.SignalReplicationPolicies;

namespace RichillCapital.Api.AcceptanceTests.SignalReplicationPolicies;

public sealed class GetSignalReplicationPolicyTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ReturnSignalReplicationPolicy()
    {
        var policyId = "1";

        var policy = await Client.GetFromJsonAsync<SignalReplicationPolicyDetailsResponse>(
            $"api/v1/signal-replication-policies/{policyId}");

        policy.Should().NotBeNull();
        policy!.Id.Should().Be(policyId);
    }
}