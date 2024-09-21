using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Domain.Brokerages;

namespace RichillCapital.Api.AcceptanceTests.Brokerages;

public sealed class DeleteBrokerageTests(
    EndToEndTestWebApplicationFactory factory) :
    BrokerageTests(factory)
{
    [Fact]
    public async Task Should_DeleteBrokerage()
    {
        var response = await Client.DeleteAsync($"api/v1/brokerages/{ExistingConnectionName}");

        response.EnsureSuccessStatusCode();

        var expectedError = BrokerageErrors.NotFound(ExistingConnectionName);
        var getResponse = await Client.GetAsync($"api/v1/brokerages/{ExistingConnectionName}");

        var problem = await getResponse.Content.ReadFromJsonAsync<ProblemDetails>();

        problem.Should().NotBeNull();
        problem!.Status.Should().Be((int)HttpStatusCode.NotFound);
    }
}