using Factory.Application.PostProdDTO;
using FluentValidation;

namespace Factory.Application.Validators
{
    public class ProdValidator : AbstractValidator<ProdDTO>
    {
        public ProdValidator()
        {
            
            RuleFor(p => p.Length).GreaterThan(0);
            RuleFor(p => p.Width).GreaterThan(0);
            RuleFor(p => p.Height).GreaterThan(0);
        }
    }
}
