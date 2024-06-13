using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Users;
using RichillCapital.Persistence.Seeds;

namespace RichillCapital.Api.AcceptanceTests.Users;

public sealed class ListUsersTests(
    AcceptanceTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task When_GivenValidRequest_Should_ReturnUsers()
    {
        var response = await Client.GetAsync("api/v1/users");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var pagedUsers = await response.Content.ReadFromJsonAsync<Paged<UserResponse>>();
        pagedUsers.Should().NotBeNull();
        pagedUsers!.Items.Should().NotBeEmpty().And.HaveCount(Seed.CreateUsers().Count());
    }
}