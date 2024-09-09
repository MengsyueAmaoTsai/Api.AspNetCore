using System.Net.Http.Json;

namespace RichillCapital.Api.AcceptanceTests.Users;

public sealed class ListUsersTests(EndToEndTestWebApplicationFactory factory) : AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ListUsers()
    {
    }
}