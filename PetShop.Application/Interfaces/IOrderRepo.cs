using PetShop.Domain;

namespace PetShop.Application.Interfaces;

public interface IOrderRepo
{
    public List<Order> GetCurrentOrdersByUserId(Guid userId);
    public List<Order> GetOrdersHistoryByUserId(Guid userId);
    public Order CreateOrder(Order order);
    public List<Order> AddDateAndPriceOfOrder(Guid userId);
    public Order UpdateOrder(Order order);
    public Order DeleteOrderById(Order order);
    public void SendEmailToUser(String email);

}