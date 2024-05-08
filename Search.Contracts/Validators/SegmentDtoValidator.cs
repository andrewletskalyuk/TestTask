using FluentValidation;
using Search.Api.Modules.Dtos;

namespace Search.Contracts.Validators;

public class SegmentDtoValidator : AbstractValidator<SegmentDto>
{
	public SegmentDtoValidator()
	{
        RuleFor(x => x.X1).GreaterThan(0).WithMessage("Coordinate X1 must be greater than zero.");
        RuleFor(x => x.X2).GreaterThan(0).WithMessage("Coordinate X2 must be greater than zero.");
        RuleFor(x => x.Y1).GreaterThan(0).WithMessage("Coordinate Y1 must be greater than zero.");
        RuleFor(x => x.Y2).GreaterThan(0).WithMessage("Coordinate Y2 must be greater than zero.");
    }
}
