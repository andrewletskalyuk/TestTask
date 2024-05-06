using FluentValidation;
using Search.Api.Modules.Dtos;

namespace Search.Contracts.Validators;

public class SegmentDtoValidator : AbstractValidator<SegmentDto>
{
	public SegmentDtoValidator()
	{
        RuleFor(x => x.Width).GreaterThan(0).WithMessage("Width must be greater than zero.");
        RuleFor(x => x.Height).GreaterThan(0).WithMessage("Height must be greater than zero.");
        RuleFor(x => x.X).GreaterThan(0).WithMessage("Coordinate X must be greater than zero.");
        RuleFor(x => x.Y).GreaterThan(0).WithMessage("Coordinate Y must be greater than zero.");
    }
}
