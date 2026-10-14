using FluentValidation;

namespace UnitConverter.Application.Conversions;

public sealed class ConvertValueValidator : AbstractValidator<ConvertValueQuery>
{
    public ConvertValueValidator()
    {
        RuleFor(x => x.Value)
            .NotNull()
            .WithMessage("Value is required.")
            .Must(value => value is null || double.IsFinite(value.Value))
            .WithMessage("Value must be a finite number.");

        RuleFor(x => x.FromUnit)
            .NotEmpty()
            .WithMessage("FromUnit is required.");

        RuleFor(x => x.ToUnit)
            .NotEmpty()
            .WithMessage("ToUnit is required.");
    }
}
