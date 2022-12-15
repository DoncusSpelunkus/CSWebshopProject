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

    
    public List<Order> GetAllOrderByUserId(Guid userId)
    {
        return _orderRepository.GetAllOrdersByUserId(userId);
    }

    public Order CreateOrder(OrderDTO orderDto, Guid userId)
    {
        
        var validationResult = _validator.Validate(orderDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var order = _mapper.Map<Order>(orderDto);
        order.UserId = userId;
        return _orderRepository.CreateOrder(order);
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
       return _orderRepository.DeleteOrderById(productId, userId);
    }
}