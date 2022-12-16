﻿using MailKit.Net.Smtp;
using PetShop.Application.Interfaces;
using PetShop.Domain;
using MimeKit;

namespace PetShop.Infastructure;

public class OrderRepo : IOrderRepo

{
    private DBContext _OrderDbContext;

    public OrderRepo(DBContext orderDbContext)
    {
        _OrderDbContext = orderDbContext;
    }
    
    
    public List<Order> GetCurrentOrdersByUserId(Guid userId)
    {
        return _OrderDbContext.OrdersTable.Where(o => o.UserId == userId && o.DateOfOrder == null).ToList();
    }
    public List<Order> GetOrdersHistoryByUserId(Guid userId)
    {
        return _OrderDbContext.OrdersTable.Where(o => o.UserId == userId && o.DateOfOrder != null).ToList();
    }
    

    public Order CreateOrder(Order order)
    {
        _OrderDbContext.OrdersTable.Add(order);
        _OrderDbContext.SaveChanges();
        return order;
    }

    public List<Order> AddDateOfOrder(Guid userId)
    {
        var listOfCurrentOrders = _OrderDbContext.OrdersTable.Where(o => o.UserId == userId && o.DateOfOrder == null);
        foreach (var order in listOfCurrentOrders)
        {
            order.DateOfOrder = DateTime.Now;
            _OrderDbContext.SaveChanges();
        }
        var listOfOrderHistory = _OrderDbContext.OrdersTable.Where(o => o.UserId == userId && o.DateOfOrder != null).ToList();

        return listOfOrderHistory;
    }

    public Order UpdateOrder(Order order)
    {
        
        _OrderDbContext.OrdersTable.Update(order);
        _OrderDbContext.SaveChanges();
        return order;
    }

    public Order DeleteOrderById(int productId, Guid userid)
    {
        var order = _OrderDbContext.OrdersTable.FirstOrDefault(o => o.ProductId == productId && o.UserId == userid);
        _OrderDbContext.OrdersTable.Remove(order);
        _OrderDbContext.SaveChanges();
        return order;
    }

    public void SendEmailToUser(string email)
    {   
        // get the user's id by email
        var id = _OrderDbContext.UserTable.FirstOrDefault(u => u.Email == email).Id;
        // get the user's orders
        var UserOrderProducts = GetCurrentOrdersByUserId(id);
        var emailsend = new MimeMessage();
        emailsend.From.Add(MailboxAddress.Parse("sosugroup2022@gmail.com"));
        emailsend.To.Add(MailboxAddress.Parse(email));
        emailsend.Subject = "Order Confirmation";
        emailsend.Body = new TextPart("plain")
        {
            Text = "Your order has been confirmed" + UserOrderProducts,
        };
        using var smpt = new SmtpClient(); 
        smpt.Connect("smtp.gmail.com", 587, false);
        smpt.Authenticate("sosugroup2022@gmail.com", "rgkmsmycxkqxtexb");
        smpt.Send(emailsend);

    }
}