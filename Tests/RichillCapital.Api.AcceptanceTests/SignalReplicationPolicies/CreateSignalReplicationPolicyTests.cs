using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.SignalReplicationPolicies;

namespace RichillCapital.Api.AcceptanceTests.SignalReplicationPolicies;

public sealed class CreateSignalReplicationPolicyTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_CreateSignalReplicationPolicy()
    {
        var request = new CreateSignalReplicationPolicyRequest
        {
            UserId = "1",
            SourceId = "TV-Long-Task"
        };

        var response = await Client.PostAsJsonAsync("api/v1/signal-replication-policies", request);
        response.EnsureSuccessStatusCode();

        var created = await response.Content.ReadFromJsonAsync<SignalReplicationPolicyCreatedResponse>();

        created.Should().NotBeNull();
        created!.Id.Should().NotBeEmpty();
    }
}