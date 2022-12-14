﻿using System.ComponentModel.DataAnnotations;

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
    public List<Order> Orders { get; set; }
    
}