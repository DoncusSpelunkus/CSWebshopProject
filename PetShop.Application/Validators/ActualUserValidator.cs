using PetShop.Domain;
using FluentValidation;

namespace PetShop.Application.Validators;

public class ActualUserValidator : AbstractValidator<User> {

   
    
    public ActualUserValidator()
    {
        RuleFor(u => u.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(u => u.Email).NotEmpty().WithMessage("Email is required");
        RuleFor(u =>u.Address).NotEmpty();
        RuleFor(u => u.Phone).NotEmpty();
        RuleFor(u => u.Phone.ToString()).Length(8);
        RuleFor(u =>u.City).NotEmpty();
        RuleFor(u => u.Zip.ToString()).Length(4);

    }
    
}