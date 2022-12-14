using AutoMapper;
using FluentValidation;
using PetShop.Application.Interfaces;
using PetShop.Application.PostProdDTO;
using PetShop.Domain;

namespace PetShop.Application;

public class OrderService : IOrderService
    {
        private IOrderRepo _orderRepository;
        private IMapper _mapper;
        private IValidator<OrderDTO> _validator;

        public OrderService(IOrderRepo repository, IMapper mapper, IValidator<OrderDTO> validator) 
        {
            _orderRepository = repository;
            _mapper = mapper;
            _validator = validator;
        }
    
    
        public List<Order> GetAllOrders()
        {
            return _orderRepository.GetAllOrders();
        }
        public List<Order> GetAllOrdersByUser(Guid userId)
        {
            return _orderRepository.GetAllOrdersByUser(userId);
        }
        
        public Order CreateOrder(OrderDTO dto)
        {
            var validation = _validator.Validate(dto);
            if (!validation.IsValid)
                throw new ValidationException(validation.ToString());
            
            return _orderRepository.CreateOrder(_mapper.Map<Order>(dto));
        }

        public Order UpdateOrder(int orderId, OrderDTO dto)
        {
            var validation = _validator.Validate(dto);
            if (!validation.IsValid)
            {
                throw new ValidationException(validation.ToString());
            }
            var order = _mapper.Map<Order>(dto);
            order.Id = orderId;
            return _orderRepository.UpdateOrder(order);
                    
        }

        public Order DeleteOrderById(int orderId)
        {
            if (orderId == 0)
                throw new ValidationException("ID is invalid");
            return _orderRepository.DeleteOrderById(orderId);
        }

        public Order GetOrderById(int orderId)
        {
            if (orderId <= 0)
                throw new ValidationException("ID is invalid");
            return _orderRepository.GetOrderById(orderId);
        }
        
}
