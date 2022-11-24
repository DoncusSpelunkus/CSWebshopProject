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
        }
    }
}