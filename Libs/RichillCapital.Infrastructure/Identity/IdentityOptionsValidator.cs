using FluentValidation;

namespace RichillCapital.Infrastructure.Identity;

internal sealed class IdentityOptionsValidator : AbstractValidator<IdentityOptions>
{
    public IdentityOptionsValidator()
    {
        RuleFor(x => x.Authority)
            .NotEmpty()
            .WithMessage("Authority is required.");

        RuleFor(x => x.Audience)
            .NotEmpty()
            .WithMessage("Audience is required.");

        RuleFor(x => x.RequireHttpsMetadata)
            .NotNull()
            .WithMessage("RequireHttpsMetadata is required.");
    }
}