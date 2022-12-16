using FluentValidation;
using PetShop.Application.PostProdDTO;

namespace PetShop.Application.Validators;

public class UserloginValidator : AbstractValidator<UserLoginDTO>
{
    public UserloginValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Username is required");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        RuleFor(x => x.Password.ToString()).MinimumLength(8).WithMessage("Password must be at least 8 characters");
        RuleFor(x => x.Password.ToString()).MaximumLength(50).WithMessage("Password must be at most 20 characters");
        RuleFor(x => x.Password.ToString()).Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter");
        RuleFor(x => x.Password.ToString()).Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter");
        RuleFor(x => x.Password.ToString()).Matches("[0-9]").WithMessage("Password must contain at least one number");
        // check that password contains at least one special character. 
        RuleFor(x => x.Password.ToString()).Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one non alphanumeric character");
    }
}