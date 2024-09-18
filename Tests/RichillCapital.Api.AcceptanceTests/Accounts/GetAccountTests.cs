using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.Accounts;

namespace RichillCapital.Api.AcceptanceTests.Accounts;

public sealed class GetAccountTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ReturnAccount()
    {
        var accountId = "SIM2121844M";

        var account = await Client.GetFromJsonAsync<AccountDetailsResponse>($"api/accounts/{accountId}");

        account.Should().NotBeNull();
        account!.Id.Should().Be(accountId);
    }
}