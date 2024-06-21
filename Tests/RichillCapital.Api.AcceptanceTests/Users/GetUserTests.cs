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

    [Fact]
    public async Task When_UserExists_Should_ReturnUser()
    {
        var userId = "UID0000001";

        var response = await Client.GetAsync($"api/v1/users/{userId}");

        response.EnsureSuccessStatusCode();

        var user = await response.Content.ReadFromJsonAsync<UserDetailsResponse>();

        user!.Id.Should().Be(userId);
        user.Accounts.Should().HaveCount(2);
    }
}
