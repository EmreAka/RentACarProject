using Entity.DTOs;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation;

public class CarForAddValidator: AbstractValidator<CarForAddDto>
{
    public CarForAddValidator()
    {
        RuleFor(c => c.BrandId).NotEmpty();
        RuleFor(c => c.ColourId).NotEmpty();
        RuleFor(c => c.EngineId).NotEmpty();
        RuleFor(c => c.FuelId).NotEmpty();
        RuleFor(c => c.ModelYear).NotEmpty().GreaterThanOrEqualTo(0);
        RuleFor(c => c.DailyPrice).NotEmpty().GreaterThanOrEqualTo(0);
        RuleFor(c => c.FuelConsumption).NotEmpty().GreaterThanOrEqualTo(0);
        RuleFor(c => c.DoorNumber).NotEmpty().GreaterThanOrEqualTo(0);
        RuleFor(c => c.Description).NotEmpty().MaximumLength(500);
        RuleFor(c => c.Images).NotEmpty();
    }
}