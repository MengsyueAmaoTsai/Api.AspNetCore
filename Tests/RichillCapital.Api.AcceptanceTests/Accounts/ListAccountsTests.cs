using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.Accounts;

namespace RichillCapital.Api.AcceptanceTests.Accounts;

public sealed class ListAccountsTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ListAccounts()
    {
        var accounts = await Client.GetFromJsonAsync<IEnumerable<AccountResponse>>("api/v1/accounts");

        accounts.Should().NotBeEmpty();
    }
}