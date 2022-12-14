using PetShop.Domain;
using FluentValidation;

namespace PetShop.Application.Validators;

public class ActualSubCatValidator
{
    public class SubCatValidator : AbstractValidator<SubCategory>
    {
        public SubCatValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
        }
    }
}