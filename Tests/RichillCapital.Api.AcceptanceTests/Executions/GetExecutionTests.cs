using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.Executions;

namespace RichillCapital.Api.AcceptanceTests.Executions;

public sealed class GetExecutionTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ReturnExecution()
    {
        var executionId = "1";
        var execution = await Client.GetFromJsonAsync<ExecutionDetailsResponse>($"api/v1/executions/{executionId}");

        execution.Should().NotBeNull();
        execution!.Id.Should().Be(executionId);
    }
}