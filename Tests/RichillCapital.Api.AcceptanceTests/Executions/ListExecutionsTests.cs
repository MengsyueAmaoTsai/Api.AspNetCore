using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.Executions;

namespace RichillCapital.Api.AcceptanceTests.Executions;

public sealed class ListExecutionsTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ListInstruments()
    {
        var executions = await Client.GetFromJsonAsync<IEnumerable<ExecutionResponse>>("api/v1/executions");

        executions.Should().NotBeNullOrEmpty();
    }
}