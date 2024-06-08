using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts;
using RichillCapital.UseCases.AuditLogs;

namespace RichillCapital.Api.AcceptanceTests.AuditLogs;

public sealed class ListAuditLogsTests(
    AcceptanceTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task When_GivenValidRequest_Should_ReturnUsers()
    {
        var response = await Client.GetAsync("api/v1/audit-logs");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var pagedUsers = await response.Content.ReadFromJsonAsync<Paged<AuditLogDto>>();
        pagedUsers.Should().NotBeNull();
        pagedUsers!.Items.Should().NotBeEmpty();
    }
}