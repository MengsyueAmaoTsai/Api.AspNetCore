using FluentValidation;

namespace RichillCapital.Infrastructure.Persistence;

internal sealed class DatabaseOptionsValidator :
    AbstractValidator<DatabaseOptions>
{
    public DatabaseOptionsValidator()
    {
        RuleFor(options => options.ConnectionString)
            .NotEmpty();
    }
}