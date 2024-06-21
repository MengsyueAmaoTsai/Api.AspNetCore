using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Accounts;

namespace RichillCapital.Api.AcceptanceTests.Accounts;

public sealed class ListAccountsTests(
    AcceptanceTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ListAccounts()
    {
        var response = await Client.GetAsync("api/v1/accounts");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var pagedResult = await response.Content.ReadFromJsonAsync<Paged<AccountResponse>>();
        pagedResult.Should().NotBeNull();
        pagedResult!.Items.Should().NotBeEmpty();
    }
}