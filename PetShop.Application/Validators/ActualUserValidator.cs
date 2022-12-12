using PetShop.Domain;
using FluentValidation;

namespace PetShop.Application.Validators;

public class ActualUserValidator : AbstractValidator<User> {

   
    
    public ActualUserValidator()
    {
        RuleFor(u => u.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(u => u.Email).NotEmpty().WithMessage("Email is required");
        RuleFor(u =>u.Address).NotEmpty();
        RuleFor(u => u.PhoneNumber).NotEmpty();
        RuleFor(u => u.PhoneNumber.ToString()).Length(8);
        RuleFor(u =>u.City).NotEmpty();
        RuleFor(u => u.Zipcode.ToString()).Length(4);

    }
    
}