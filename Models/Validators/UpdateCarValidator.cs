using CarsMongoDbDemo.ViewModels.Car;
using FluentValidation;

namespace CarsMongoDbDemo.Models.Validators;

public class UpdateCarValidator : AbstractValidator<CarInputViewModel>
{
    public UpdateCarValidator()
    {
        RuleFor(x => x.VehicleBrand)
            .NotNull()
            .WithMessage("Vehicle Brand must be informed!")
            .NotEmpty()
            .WithMessage("Vehicle Brand must be informed!")
            .Length(3, 100).WithMessage("Vehicle Brand must be between 3 and 100 characters!");

        RuleFor(x => x.Name)
            .NotNull()
            .WithMessage("Car Name must be informed!")
            .NotEmpty()
            .WithMessage("Car Name must be informed!")
            .Length(3, 100).WithMessage("Car Name must be between 3 and 100 characters!");

        RuleFor(x => x.Price)
            .NotNull()
            .WithMessage("Price must be informed!")
            .NotEmpty()
            .WithMessage("Price must be informed!")
            .GreaterThanOrEqualTo(2000)
            .WithMessage($"Vehicle price must be greater than {2000:c} and less the {300000:c}!")
            .LessThanOrEqualTo(300000)
            .WithMessage($"Vehicle price must be greater than {2000:c} and less the {300000:c}!");
    }
}