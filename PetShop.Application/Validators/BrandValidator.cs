using FluentValidation;
using PetShop.Application.PostProdDTO;

namespace PetShop.Application.Validators
{
    public class BrandValidator : AbstractValidator<BrandDto>
    {
        public BrandValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
        }
        
    }
}
