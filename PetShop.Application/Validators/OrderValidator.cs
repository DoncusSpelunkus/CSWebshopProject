using FluentValidation;
using PetShop.Application.PostProdDTO;

namespace PetShop.Application.Validators;
public class ActualOrderValidator : AbstractValidator<OrderDTO>
{
    public ActualOrderValidator()
    {
        RuleFor(o => o.ProductId).NotEmpty().GreaterThan(0);
        RuleFor(o => o.Amount).GreaterThan(0);
        RuleFor(o => o.Price).GreaterThan(0);
        
    }
}