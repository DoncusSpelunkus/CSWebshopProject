using Factory.Domain;
using FluentValidation;

namespace PetShop.Application.Validators;

public class ActualUserValidator : AbstractValidator<User> {

   
    
    public ActualUserValidator()
    {
        RuleFor(u => u.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(u => u.Email).NotEmpty().WithMessage("Email is required");

    }
    
}