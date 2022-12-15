using Microsoft.EntityFrameworkCore;
using PetShop.Application.Interfaces;
using PetShop.Domain;

namespace PetShop.Infastructure;

public class HistoryOrderRepo : IHistyoryOrderRepo

{
    private DBContext _orderDbContext;

    public HistoryOrderRepo(DBContext orderDbContext)
    {
        _orderDbContext = orderDbContext;
    }
    
    
    public List<HistoryOrder> GetAllOrders()
    {
        var orders = _orderDbContext.HistoryOrderTable.ToList();
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
    
    public List<HistoryOrder> GetAllOrdersByUser(Guid userId)
    {
        var listOfOrders = _orderDbContext.HistoryOrderTable.ToList();
        var listOfOrdersByUser = new List<HistoryOrder>();
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

    public HistoryOrder CreateOrder(HistoryOrder historyOrder)
    {
        foreach (var orderedProd in historyOrder.OrderedProductsList)
        {
            orderedProd.OrderId = historyOrder.Id;
            _orderDbContext.OrderedProductsTable.Add(orderedProd);
        }
        _orderDbContext.HistoryOrderTable.Add(historyOrder);
        _orderDbContext.SaveChanges();
        return historyOrder;
    }

    public HistoryOrder UpdateOrder(HistoryOrder historyOrder)
    {
        var orderedProdList = _orderDbContext.OrderedProductsTable.AsNoTracking().ToList();
        foreach (var orderedProd in orderedProdList)
        {
            if (orderedProd.OrderId == historyOrder.Id)
                _orderDbContext.OrderedProductsTable.Remove(orderedProd);
        }
        
        foreach (var orderedProd in historyOrder.OrderedProductsList)
        {
            orderedProd.OrderId = historyOrder.Id;
            _orderDbContext.OrderedProductsTable.Add(orderedProd);
        }
        
        _orderDbContext.HistoryOrderTable.Update(historyOrder);
        _orderDbContext.SaveChanges();
        return historyOrder;
    }

    public HistoryOrder DeleteOrderById(int orderId)
    {
        HistoryOrder historyOrder = GetOrderById(orderId);
        _orderDbContext.HistoryOrderTable.Remove(historyOrder);
        _orderDbContext.SaveChanges();
        return historyOrder;
    }

    public HistoryOrder GetOrderById(int orderId)
    {
        HistoryOrder historyOrder = new HistoryOrder();
        var orderedProdList = _orderDbContext.OrderedProductsTable.ToList();
        var orderedProducts = new List<OrderedProducts>();
        foreach (var orderedProd in orderedProdList)
        {
            if(orderedProd.OrderId == orderId)
                orderedProducts.Add(orderedProd);
        }
        historyOrder = _orderDbContext.HistoryOrderTable.FirstOrDefault(s => s.Id == orderId);
        historyOrder.OrderedProductsList = orderedProducts;

        return historyOrder;


    }
}