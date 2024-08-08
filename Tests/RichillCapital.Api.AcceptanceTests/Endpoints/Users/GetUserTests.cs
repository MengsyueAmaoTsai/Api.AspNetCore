using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.Users;
using RichillCapital.Persistence.Seeds;

namespace RichillCapital.Api.AcceptanceTests.Endpoints.Users;

public sealed class GetUserTests(
    AcceptanceTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ReturnUser()
    {
        var expected = Seed.CreateUsers().First();

        var user = await Client.GetFromJsonAsync<UserDetailsResponse>($"api/v1/users/{expected.Id}");

        user.Should().NotBeNull();
        user!.Id.Should().Be(expected.Id.Value);
    }
}
