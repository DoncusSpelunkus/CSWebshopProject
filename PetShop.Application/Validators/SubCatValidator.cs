using FluentValidation;
using PetShop.Application.PostProdDTO;

namespace PetShop.Application.Validators
{
    public class SubCatValidator : AbstractValidator<SubCatDTO>
    {
        public SubCatValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
        }
    }
}