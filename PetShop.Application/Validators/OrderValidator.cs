using Factory.Application.PostProdDTO;
using FluentValidation;
using PetShop.Application.PostProdDTO;
using PetShop.Domain;


namespace PetShop.Application.Validators;

public class OrderValidator :AbstractValidator<HistoryOrderDTO>
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

public class ActualOrderValidator : AbstractValidator<OrderDTO>
{
    public ActualOrderValidator()
    {
        RuleFor(o => o.ProdouctId).NotEmpty().GreaterThan(0);
        RuleFor(o => o.Amount).GreaterThan(0);
        
    }
}