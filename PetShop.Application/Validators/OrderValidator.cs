using FluentValidation;
using PetShop.Application.PostProdDTO;


namespace PetShop.Application.Validators;

public class OrderValidator :AbstractValidator<OrderDTO>
{

    public OrderValidator()
    {
        RuleFor(o => o.UserId).NotEmpty();
        RuleFor(o => o.DateOfOrder).NotEmpty();
        RuleFor(o => o.OrderedProductsList).NotEmpty();
    }
    
}

public class ListOfProductsValidator :AbstractValidator<OrderedProductsDTO>
{

    public ListOfProductsValidator()
    {
        RuleFor(o => o.ProductId).GreaterThan(0);
        RuleFor(o => o.Amount).GreaterThan(0);
    }
    
}