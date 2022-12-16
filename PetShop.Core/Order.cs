
using System.ComponentModel.DataAnnotations;

namespace PetShop.Domain;
public class Order
{
    [Key]
    public int Id { get; set; }
    
    public List<OrderedProducts> OrderedProductsList { get; set; }
    public User? User { get; set; }
    public Guid UserId { get; set; }
    
    public DateTime DateOfOrder { get; set; }
    
    
}



public class OrderedProducts
{
    public Order? Order { get; set; }
    public int OrderId { get; set; }
    public Product? Product { get; set; }
    public int ProductId { get; set; }
    public int Amount { get; set; }
}