using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts.Users;

namespace RichillCapital.Api.AcceptanceTests.Users;

public sealed class GetUserTests(
    AcceptanceTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task When_UserNotExists_Should_ReturnNotFound()
    {
        var userId = "UID9999999";

        var response = await Client.GetAsync($"api/v1/users/{userId}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();

        problem!.Title.Should().Be("Users.NotFound");
    }
}
