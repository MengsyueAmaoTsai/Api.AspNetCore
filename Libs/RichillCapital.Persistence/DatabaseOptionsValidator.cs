using FluentValidation;

namespace RichillCapital.Persistence;

internal sealed class DatabaseOptionsValidator :
    AbstractValidator<DatabaseOptions>
{
    public DatabaseOptionsValidator()
    {
        RuleFor(options => options.ConnectionString)
            .NotEmpty()
            .WithMessage("Connection string is required.");
    }
}