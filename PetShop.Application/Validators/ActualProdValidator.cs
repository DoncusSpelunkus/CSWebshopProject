using FluentValidation;
using PetShop.Domain;

namespace PetShop.Application.Validators;

public class ActualProdValidator
{
    public class ProdValidator : AbstractValidator<Product>
    {
        public ProdValidator()
        {
            RuleFor(p => p.ID).GreaterThan(0);
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Price).GreaterThan(0);
            RuleFor(p => p.Description).NotEmpty();
            RuleFor(p => p.ImageUrl).NotEmpty();
            RuleFor(p => p.Rating).GreaterThan(0);
            RuleFor(p => p.MainCategoryObj).NotEmpty();
            RuleFor(p => p.SubCategoryObj).NotEmpty();
            RuleFor(p => p.Brand).NotEmpty();
        }
    }
}