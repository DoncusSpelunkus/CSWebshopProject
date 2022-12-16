using Factory.Application.PostProdDTO;
using PetShop.Domain;

namespace PetShop.Application.Interfaces;

public interface IOrderService
{
    public List<Order> GetCurrentOrderByUserId(Guid userId);
    public List<Order> GetOrdersHistoryByUserId(Guid userId);
    public Order CreateOrder(OrderDTO orderDto, Guid userId);
    public List<Order> AddDateOfOrder(Guid userId);

    public Order UpdateOrder(Guid userId, OrderDTO dto);
        
    public Order DeleteOrder(int productId, Guid userId );
    
    public void SendEmailtoUser(string email);

}