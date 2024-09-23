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
        var accountId = "000-8283782";

        var account = await Client.GetFromJsonAsync<AccountDetailsResponse>($"api/v1/accounts/{accountId}");

        account.Should().NotBeNull();
        account!.Id.Should().Be(accountId);
    }
}