using PetShop.Domain;

namespace PetShop.Application.Interfaces;

public interface IOrderRepo
{
    public List<Order> GetAllOrders();
    public Order CreateOrder(Order order);
    public Order UpdateOrder(Order order);
    public Order DeleteOrderById(int id);
    public Order GetOrderById(int orderId);
    public List<Order> GetAllOrdersByUser(Guid userId);
}