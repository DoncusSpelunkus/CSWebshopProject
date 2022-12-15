using Factory.Application.PostProdDTO;
using PetShop.Domain;

namespace PetShop.Application.Interfaces;

public interface IOrderService
{
    public List<Order> GetAllOrderByUserId(Guid userId);
        
    public Order CreateOrder(OrderDTO orderDto, Guid userId);

    public Order UpdateOrder(Guid userId, OrderDTO dto);
        
    public Order DeleteOrder(int productId, Guid userId );
    
}