using System.Data;
using FluentValidation;
using PetShop.Application.PostProdDTO;

namespace PetShop.Application.Validators;

public class CategoryValidator
{
    public class MainCatValidator : AbstractValidator<MainCatDTO>
    {
        public MainCatValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.ProdList);
        }
    }
}