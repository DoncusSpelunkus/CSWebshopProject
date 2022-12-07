using PetShop.Domain;
using FluentValidation;

namespace PetShop.Application.Validators;

public class ActualBrandValidator
{
    public class BrandValidator : AbstractValidator<MainCategory>
    {
        public BrandValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
        }
    }
}