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
        RuleFor(u => u.Phone.ToString()).Length(8);
        RuleFor(u =>u.City).NotEmpty();
        RuleFor(u => u.Zip.ToString()).Length(4);
    }
}