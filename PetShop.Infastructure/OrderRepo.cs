using Microsoft.EntityFrameworkCore;
using PetShop.Application.Interfaces;
using PetShop.Domain;

namespace PetShop.Infastructure;

public class OrderRepo : IOrderRepo

{
    private DBContext _orderDbContext;

    public OrderRepo(DBContext orderDbContext)
    {
        _orderDbContext = orderDbContext;
    }
    
    
    public List<Order> GetAllOrders()
    {
        var orders = _orderDbContext.OrderTable.ToList();
        var orderedProducts = _orderDbContext.OrderedProductsTable.ToList();


        foreach (var order in orders)
        {
            var orderedProdList = new List<OrderedProducts>();
            foreach (var orderedProd in orderedProducts)
            {
             if(orderedProd.OrderId == order.Id)
                 orderedProdList.Add(orderedProd);
            }

            order.OrderedProductsList = orderedProdList;
        }

        return orders;
    }
    
    public List<Order> GetAllOrdersByUser(Guid userId)
    {
        var listOfOrders = _orderDbContext.OrderTable.ToList();
        var listOfOrdersByUser = new List<Order>();
        var orderedProdList = _orderDbContext.OrderedProductsTable.ToList();
        
        foreach (var order in listOfOrders)
        {
            var listOfOrderedProd = new List<OrderedProducts>();
            if(userId == order.UserId)
                listOfOrdersByUser.Add(order);
            foreach (var orderedProd in orderedProdList)
            {
                if(order.Id == orderedProd.OrderId)
                    listOfOrderedProd.Add(orderedProd);
            }

            order.OrderedProductsList = listOfOrderedProd;
        }

        return listOfOrdersByUser;
    }

    public Order CreateOrder(Order order)
    {
        foreach (var orderedProd in order.OrderedProductsList)
        {
            orderedProd.OrderId = order.Id;
            _orderDbContext.OrderedProductsTable.Add(orderedProd);
        }
        _orderDbContext.OrderTable.Add(order);
        _orderDbContext.SaveChanges();
        return order;
    }

    public Order UpdateOrder(Order order)
    {
        var orderedProdList = _orderDbContext.OrderedProductsTable.AsNoTracking().ToList();
        foreach (var orderedProd in orderedProdList)
        {
            if (orderedProd.OrderId == order.Id)
                _orderDbContext.OrderedProductsTable.Remove(orderedProd);
        }
        
        foreach (var orderedProd in order.OrderedProductsList)
        {
            orderedProd.OrderId = order.Id;
            _orderDbContext.OrderedProductsTable.Add(orderedProd);
        }
        
        _orderDbContext.OrderTable.Update(order);
        _orderDbContext.SaveChanges();
        return order;
    }

    public Order DeleteOrderById(int orderId)
    {
        Order order = GetOrderById(orderId);
        _orderDbContext.OrderTable.Remove(order);
        _orderDbContext.SaveChanges();
        return order;
    }

    public Order GetOrderById(int orderId)
    {
        Order order = new Order();
        var orderedProdList = _orderDbContext.OrderedProductsTable.ToList();
        var orderedProducts = new List<OrderedProducts>();
        foreach (var orderedProd in orderedProdList)
        {
            if(orderedProd.OrderId == orderId)
                orderedProducts.Add(orderedProd);
        }
        order = _orderDbContext.OrderTable.FirstOrDefault(s => s.Id == orderId);
        order.OrderedProductsList = orderedProducts;

        return order;


    }
}