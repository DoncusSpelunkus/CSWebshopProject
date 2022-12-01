    
using FluentValidation;
using PetShop.Domain;

namespace PetShop.Application.Validators;

public class ActualSpecValidator
{
    public class SpecValidator : AbstractValidator<Specs>
    {
        public SpecValidator()
        {
            RuleFor(s => s.ID).GreaterThan(0);
            RuleFor(s => s.SpecName).NotEmpty();
        }
    }
}