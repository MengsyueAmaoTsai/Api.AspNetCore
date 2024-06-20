using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Users;

namespace RichillCapital.Api.AcceptanceTests.Users;

public sealed class ListUsersTests(
    AcceptanceTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task When_GivenValidRequest_Should_ListUsers()
    {
        var response = await Client.GetAsync("api/v1/users");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var pagedResponse = await response.Content.ReadFromJsonAsync<Paged<UserResponse>>();
        
        pagedResponse.Should().NotBeNull();
        pagedResponse!.Items.Should().NotBeEmpty();
    }
}
