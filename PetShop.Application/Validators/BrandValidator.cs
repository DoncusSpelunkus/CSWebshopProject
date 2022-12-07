using FluentValidation;
using PetShop.Application.PostProdDTO;

namespace PetShop.Application.Validators
{
    public class BrandValidator : AbstractValidator<SubCatDTO>
    {
        public BrandValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
        }
    }
}