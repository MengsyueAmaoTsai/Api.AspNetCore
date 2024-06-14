using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.Users;

namespace RichillCapital.Api.AcceptanceTests.Users;

public sealed class GetUserTests(
    AcceptanceTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task When_GivenValidRequest_Should_ReturnUser()
    {
        // Get the admin user
        var adminUserId = "UID0000001";
        var adminEmail = "mengsyue.tsai@outlook.com";

        var response = await Client.GetAsync($"api/v1/users/{adminUserId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var user = await response.Content.ReadFromJsonAsync<UserDetailsResponse>();

        user.Should().NotBeNull();
        user!.Id.Should().Be(adminUserId);
        user!.Email.Should().Be(adminEmail);
    }
}