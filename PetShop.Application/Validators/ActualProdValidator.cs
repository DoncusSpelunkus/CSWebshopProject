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
            RuleFor(p => p.MainCategoryObj).NotEmpty();
            RuleFor(p => p.SubCategoryObj).NotEmpty();
            RuleFor(p => p.Brand).NotEmpty();
        }
    }
    public class RatingValidator: AbstractValidator<Rating>
    {
        public RatingValidator()
        {
            RuleFor(r => r.RatingValue).GreaterThan(0);
            RuleFor(r => r.RatingValue).LessThan(6);
            RuleFor(r => r.RatingValue).NotEmpty();
            
        }
    }
}