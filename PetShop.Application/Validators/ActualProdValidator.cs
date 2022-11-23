using Factory.Domain;
using FluentValidation;

namespace Factory.Application.Validators;

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