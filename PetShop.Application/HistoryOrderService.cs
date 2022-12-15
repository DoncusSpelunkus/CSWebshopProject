using AutoMapper;
using FluentValidation;
using PetShop.Application.Interfaces;
using PetShop.Application.PostProdDTO;
using PetShop.Domain;

namespace PetShop.Application;

public class HistyoryOrderService : IHistyoryOrderService
    {
        private IHistyoryOrderRepo _orderRepository;
        private IMapper _mapper;
        private IValidator<HistoryOrderDTO> _validator;

        public HistyoryOrderService(IHistyoryOrderRepo repository, IMapper mapper, IValidator<HistoryOrderDTO> validator) 
        {
            _orderRepository = repository;
            _mapper = mapper;
            _validator = validator;
        }
    
    
        public List<HistoryOrder> GetAllOrders()
        {
            return _orderRepository.GetAllOrders();
        }
        public List<HistoryOrder> GetAllOrdersByUser(Guid userId)
        {
            return _orderRepository.GetAllOrdersByUser(userId);
        }
        
        public HistoryOrder CreateOrder(HistoryOrderDTO dto)
        {
            var validation = _validator.Validate(dto);
            if (!validation.IsValid)
                throw new ValidationException(validation.ToString());
            
            return _orderRepository.CreateOrder(_mapper.Map<HistoryOrder>(dto));
        }

        public HistoryOrder UpdateOrder(int orderId, HistoryOrderDTO dto)
        {
            var validation = _validator.Validate(dto);
            if (!validation.IsValid)
            {
                throw new ValidationException(validation.ToString());
            }
            var order = _mapper.Map<HistoryOrder>(dto);
            order.Id = orderId;
            return _orderRepository.UpdateOrder(order);
                    
        }

        public HistoryOrder DeleteOrderById(int orderId)
        {
            if (orderId == 0)
                throw new ValidationException("ID is invalid");
            return _orderRepository.DeleteOrderById(orderId);
        }

        public HistoryOrder GetOrderById(int orderId)
        {
            if (orderId <= 0)
                throw new ValidationException("ID is invalid");
            return _orderRepository.GetOrderById(orderId);
        }
        
}
