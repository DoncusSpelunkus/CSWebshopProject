using System.ComponentModel.DataAnnotations;

namespace PetShop.Domain;

public class User

{   

    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public byte[] HashPassword { get; set; }
    public byte[] SaltPassword { get; set; }
    public int type { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public int Zip { get; set; }
    public int Phone { get; set; }
    public List<Rating> Ratings { get; set; }
    public List<Order> CurrentOrderList { get; set; }

}

public class Order
{   
    public int Id { get; set; }
    // foreign key
    public int  ProductId { get; set; }
    public Guid? UserId { get; set; }
    
    public int Amount { get; set; }
    
    public DateTime? DateOfOrder { get; set; }
    public int OrderId { get; set; }
    public double Price { get; set; }

    //navigation purpose
    public Product? Product { get; set; }
    public User? User { get; set; }
    
    
}
