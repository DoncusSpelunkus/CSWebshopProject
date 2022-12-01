using FluentValidation;
using PetShop.Application.PostProdDTO;


namespace PetShop.Application.Validators;

public class SpecValidator :AbstractValidator<SpecDTO>
{

    public SpecValidator()
    {
        RuleFor(s => s.SpecName).NotEmpty();
    }
    
}