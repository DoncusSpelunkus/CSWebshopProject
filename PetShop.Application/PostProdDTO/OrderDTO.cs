namespace PetShop.Application;

public class OrderDTO
{
    public Guid UserId { get; set; }

    public DateTime DateOfOrder { get; set; }
    
    public List<OrderedProductsDTO> OrderedProductsList { get; set; }
}


public class OrderedProductsDTO
{
    public int ProductId { get; set; }
    public int Amount { get; set; }
}