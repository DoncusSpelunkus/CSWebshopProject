using Factory.Domain;
using FluentValidation;

namespace PetShop.Application.Validators;

public class ActualBrandValidator
{
    public class BrandValidator : AbstractValidator<Brand>
    {
        public BrandValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
        }
    }
}