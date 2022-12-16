using PetShop.Application.PostProdDTO;
using PetShop.Domain;

namespace PetShop.Application.Interfaces;

public interface IOrderService
{
    public List<Order> GetAllOrders();
    public Order CreateOrder(OrderDTO order);
    public Order UpdateOrder(int orderId, OrderDTO order);
    public Order DeleteOrderById(int id);
    public Order GetOrderById(int orderId);
    public List<Order> GetAllOrdersByUser(Guid userId);
}