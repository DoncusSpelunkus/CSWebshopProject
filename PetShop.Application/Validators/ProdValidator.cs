using FluentValidation;
using PetShop.Application.PostProdDTO;

namespace PetShop.Application.Validators
{
    public class ProdValidator : AbstractValidator<ProdDTO>
    {
        public ProdValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Price).GreaterThan(0);
            RuleFor(p => p.Description).NotEmpty();
            RuleFor(p => p.ImageUrl).NotEmpty();
            RuleFor(p => p.MainCategoryID).NotEmpty();
            RuleFor(p => p.SubCategoryID).NotEmpty();
            RuleFor(p => p.BrandID).NotEmpty();
        }
    }
}
