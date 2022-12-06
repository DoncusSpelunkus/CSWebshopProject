using FluentValidation;
using PetShop.Application.PostProdDTO;

namespace PetShop.Application.Validators
{
    public class MainCatValidator : AbstractValidator<MainCatDTO>
    {
        public MainCatValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
        }
        
    }
}