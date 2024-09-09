using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.Users;

namespace RichillCapital.Api.AcceptanceTests.Users;

public sealed class GetUserTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ReturnUser()
    {
        var expectedId = "1";
        var user = await Client.GetFromJsonAsync<UserDetailsResponse>($"api/v1/users/{expectedId}");

        user.Should().NotBeNull();
        user!.Id.Should().Be(expectedId);
    }
}