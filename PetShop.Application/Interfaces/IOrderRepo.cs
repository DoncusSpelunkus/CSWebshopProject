using PetShop.Domain;

namespace PetShop.Application.Interfaces;

public interface IOrderRepo
{
    public List<Order> GetAllOrdersByUserId(Guid userId);
    public Order CreateOrder(Order order);
    public Order UpdateOrder(Order order);
    public Order DeleteOrderById(int productId, Guid userId);
    public void SendEmailToUser(String email);
  

}