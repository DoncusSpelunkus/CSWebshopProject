using PetShop.Domain;
using FluentValidation;

namespace PetShop.Application.Validators;

public class ActualMainCatValidator
{
    public class MainCatValidator : AbstractValidator<MainCategory>
    {
        public MainCatValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
        }
    }
}