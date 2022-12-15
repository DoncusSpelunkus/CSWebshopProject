using PetShop.Application.Interfaces;
using PetShop.Domain;

namespace PetShop.Infastructure;

public class OrderRepo : IOrderRepo

{
    private DBContext _OrderDbContext;

    public OrderRepo(DBContext orderDbContext)
    {
        _OrderDbContext = orderDbContext;
    }
    
    
    public List<Order> GetAllOrdersByUserId(Guid userId)
    {
        return _OrderDbContext.OrdersTable.Where(o => o.UserId == userId).ToList();
    }
    

    public Order CreateOrder(Order order)
    {
        _OrderDbContext.OrdersTable.Add(order);
        _OrderDbContext.SaveChanges();
        return order;
    }

    public Order UpdateOrder(Order order)
    {
        
        _OrderDbContext.OrdersTable.Update(order);
        _OrderDbContext.SaveChanges();
        return order;
    }

    public Order DeleteOrderById(int productId, Guid userid)
    {
        var order = _OrderDbContext.OrdersTable.FirstOrDefault(o => o.ProdouctId == productId && o.UserId == userid);
        _OrderDbContext.OrdersTable.Remove(order);
        _OrderDbContext.SaveChanges();
        return order;
    }

  
}