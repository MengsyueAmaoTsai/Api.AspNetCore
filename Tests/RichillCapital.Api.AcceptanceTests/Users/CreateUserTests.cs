using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.Users;

namespace RichillCapital.Api.AcceptanceTests.Users;

public sealed class CreateUserTests(
    AcceptanceTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task When_GivenValidRequest_Should_CreateUser()
    {
        var expectedName = "Test user";
        var expectedEmail = "test@gmail.com";
        var expectedPassword = "random-generated-password";

        var createResponse = await Client.PostAsJsonAsync("api/v1/users", new CreateUserRequest
        {
            Name = expectedName,
            Email = expectedEmail,
            Password = expectedPassword,
        });

        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await createResponse.Content.ReadFromJsonAsync<CreateUserResponse>();
        result.Should().NotBeNull();
        result!.Id.Should().NotBeEmpty();

        var getUserResponse = await Client.GetFromJsonAsync<UserDetailsResponse>($"api/v1/users/{result.Id}");
        getUserResponse.Should().NotBeNull();
        getUserResponse!.Id.Should().Be(result.Id);
        getUserResponse!.Name.Should().Be(expectedName);
        getUserResponse!.Email.Should().Be(expectedEmail);
    }
}