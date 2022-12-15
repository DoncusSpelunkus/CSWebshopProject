using PetShop.Domain;

namespace PetShop.Application.Interfaces;

public interface IHistyoryOrderRepo
{
    public List<HistoryOrder> GetAllOrders();
    public HistoryOrder CreateOrder(HistoryOrder historyOrder);
    public HistoryOrder UpdateOrder(HistoryOrder historyOrder);
    public HistoryOrder DeleteOrderById(int id);
    public HistoryOrder GetOrderById(int orderId);
    public List<HistoryOrder> GetAllOrdersByUser(Guid userId);
}