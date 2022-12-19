using System.Data;
using System.Reflection.Metadata;
using FluentValidation;
using PetShop.Application.PostProdDTO;

namespace PetShop.Application.Validators;

public class UserValidator : AbstractValidator<UserDTO>
{
    public UserValidator()
    {
        RuleFor(u => u.Name).NotEmpty();
        RuleFor(u => u.Email).NotEmpty().EmailAddress();
        RuleFor(u =>u.Address).NotEmpty();
        RuleFor(u => u.Phone).NotEmpty();
        RuleFor(u => u.Phone.ToString()).MinimumLength(8);
        RuleFor(u =>u.City).NotEmpty();
        RuleFor(u => u.Zip.ToString()).Length(4);
        RuleFor(x => x.Email).NotEmpty().WithMessage("Username is required");
        RuleFor(x => x.password).NotEmpty().WithMessage("Password is required");
        RuleFor(x => x.password.ToString()).MinimumLength(8).WithMessage("Password must be at least 8 characters");
        RuleFor(x => x.password.ToString()).MaximumLength(50).WithMessage("Password must be at most 20 characters");
        RuleFor(x => x.password.ToString()).Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter");
        RuleFor(x => x.password.ToString()).Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter");
        RuleFor(x => x.password.ToString()).Matches("[0-9]").WithMessage("Password must contain at least one number");
        // check that password contains at least one special character. 
        RuleFor(x => x.password.ToString()).Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one non alphanumeric character");
    }
}   