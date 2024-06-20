using RichillCapital.UseCases.Users.List;

namespace RichillCapital.Contracts.Users;

public sealed record ListUsersRequest
{
}

public static class ListUsersRequestMapping
{
    public static ListUsersQuery ToQuery(this ListUsersRequest request) =>
        new();
}