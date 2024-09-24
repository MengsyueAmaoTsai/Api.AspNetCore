using System.Net.Http.Json;

using FluentAssertions;
using FluentAssertions.Extensions;

using RichillCapital.Contracts.Accounts;
using RichillCapital.Domain;

namespace RichillCapital.Api.AcceptanceTests.Accounts;

public sealed class CreateAccountTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    private static readonly CreateAccountRequest ValidRequest = new()
    {
        UserId = "1",
        ConnectionName = "RichillCapital.Exchange",
        Alias = "EndToEndTest",
        Currency = Currency.TWD.Name,
    };

    [Fact]
    public async Task Should_CreateAccount()
    {
        var response = await Client.PostAsJsonAsync("api/v1/accounts", ValidRequest);
        response.EnsureSuccessStatusCode();

        var created = await response.Content.ReadFromJsonAsync<AccountCreatedResponse>();

        created.Should().NotBeNull();
        created!.Id.Should().NotBeEmpty();

        var account = await Client.GetFromJsonAsync<AccountResponse>($"api/v1/accounts/{created!.Id}");
        account.Should().NotBeNull();
        account!.Id.Should().Be(created!.Id);
        account!.UserId.Should().Be(ValidRequest.UserId);
        account!.ConnectionName.Should().Be(ValidRequest.ConnectionName);
        account!.Alias.Should().Be(ValidRequest.Alias);
        account!.Currency.Should().Be(ValidRequest.Currency);
        account!.CreatedTimeUtc.Should().BeCloseTo(DateTimeOffset.UtcNow, 2.Seconds());
    }
}