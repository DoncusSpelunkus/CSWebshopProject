using AutoMapper;
using Factory.Application.PostProdDTO;
using FluentValidation;
using PetShop.Application.Interfaces;
using PetShop.Domain;

namespace PetShop.Application;

public class OrderService : IOrderService

{
    
    private readonly IOrderRepo _orderRepository;
    private readonly IMapper _mapper;
    private IValidator<OrderDTO> _validator;
    private IOrderService _orderServiceImplementation;

    public OrderService(IOrderRepo orderRepository, IMapper mapper, IValidator<OrderDTO> validator)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _validator = validator;
    }

    
    public List<Order> GetCurrentOrderByUserId(Guid userId)
    {
        return _orderRepository.GetCurrentOrdersByUserId(userId);
    }

    public List<Order> GetOrdersHistoryByUserId(Guid userId)
    {
        return _orderRepository.GetOrdersHistoryByUserId(userId);
    }
    public Order CreateOrder(OrderDTO orderDto, Guid userId)
    {
        
        var validationResult = _validator.Validate(orderDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var orderList = _orderRepository.GetCurrentOrdersByUserId(userId);
        foreach (var o in orderList)
        {
            if (o.ProductId == orderDto.ProductId) 
             throw new ValidationException("This product is already in your cart");
        }
        
        var order = _mapper.Map<Order>(orderDto);
        order.UserId = userId;
        return _orderRepository.CreateOrder(order);
    }
    public List<Order> AddDateAndPriceOfOrder(Guid userId)
    {
        return _orderRepository.AddDateAndPriceOfOrder(userId);
    }

    public Order UpdateOrder(Guid userId, OrderDTO dto)
    
    {
        var validationResult = _validator.Validate(dto);
        if (!validationResult.IsValid)  
        {
            throw new ValidationException(validationResult.ToString());
        }
        
        var order = _mapper.Map<Order>(dto);
        order.UserId = userId;
        return _orderRepository.UpdateOrder(order);
    }

    public Order DeleteOrder(int productId, Guid userId)
    {
        var orderList = _orderRepository.GetCurrentOrdersByUserId(userId);
        var order = new Order();
        foreach (var o in orderList)
        {
            if (o.ProductId == productId)
            {
                order = o;
            }
        }
        
        if(order.ProductId != productId)
            throw new ValidationException("You don't have product with this id " +productId + " in your cart");
        
        return _orderRepository.DeleteOrderById(order);
    }

    public void SendEmailtoUser(string email)
    {
        _orderRepository.SendEmailToUser(email);
    }
}