using System.Data;
using FluentValidation;
using PetShop.Application.PostProdDTO;

namespace PetShop.Application.Validators;

public class SubCategoryValidator
{
    public class SubCatValidator : AbstractValidator<MainCatDTO>
    {
        public SubCatValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.ProdList);
        }
    }
}