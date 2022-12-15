using PetShop.Application.PostProdDTO;
using PetShop.Domain;

namespace PetShop.Application.Interfaces;

public interface IHistyoryOrderService
{
    public List<HistoryOrder> GetAllOrders();
    public HistoryOrder CreateOrder(HistoryOrderDTO order);
    public HistoryOrder UpdateOrder(int orderId, HistoryOrderDTO order);
    public HistoryOrder DeleteOrderById(int id);
    public HistoryOrder GetOrderById(int orderId);
    public List<HistoryOrder> GetAllOrdersByUser(Guid userId);
}