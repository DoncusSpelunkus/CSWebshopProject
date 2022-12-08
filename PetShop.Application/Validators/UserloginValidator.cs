using FluentValidation;
using PetShop.Application.PostProdDTO;

namespace PetShop.Application.Validators;

public class UserloginValidator : AbstractValidator<UserLoginDTO>
{
    public UserloginValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
    }
}