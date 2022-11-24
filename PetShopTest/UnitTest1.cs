using Factory.Application;
using Factory.Application.Interfaces;
using Factory.Domain;
using Moq;

namespace PetShopTest;

public class UnitTest1
{
    [Fact]
    public void GetProductByIDTest()
    {
        Mock<IShopRepo> orderRepository = new Mock<IShopRepo>();

        Product expected = new Product()
        {
            ID = 1, Name = "mockFood1", Price = 10, Description = "very good product1", ImageUrl = "fakeURL1",
            Rating = 1.5, MainCategory = 0, SubCategory = 0, Brand = 0
        };
        orderRepository.Setup(repo => repo.GetProductByID(1)).Returns(expected);
        
        Product actual = new Product()
        {
            ID = 1, Name = "mockFood1", Price = 10, Description = "very good product1", ImageUrl = "fakeURL1",
            Rating = 1.5, MainCategory = 0, SubCategory = 0, Brand = 0
        };
        // Assert
        Assert.Equivalent(expected,actual);
        
    
    }

    // test for delete 
    [Fact]
    public void DeleteByID_ValidData()
    {

        Mock<IShopRepo> orderRepository = new Mock<IShopRepo>();
        int Id = 1;
        Product product = new Product()
        {
            ID = 1, Name = "mockFood1", Price = 10, Description = "very good product1", ImageUrl = "fakeURL1",
            Rating = 1.5, MainCategory = 0, SubCategory = 0, Brand = 0
        };


        orderRepository.Setup(repo => repo.DeleteProduct(Id)).Returns(product);

        IShopRepo orderService =  orderRepository.Object;


        var actual = orderService.DeleteProduct(Id);
        Assert.Equal(product, actual);
    }


    [Fact]
    public void CreateProduct_ValidData()
    {

        Mock<IShopRepo> orderRepository = new Mock<IShopRepo>();
        Product product = new Product()
        {
            ID = 1, Name = "mockFood1", Price = 10, Description = "very good product1", ImageUrl = "fakeURL1",
            Rating = 1.5, MainCategory = 0, SubCategory = 0, Brand = 0
        };


        orderRepository.Setup(repo => repo.CreateProduct(product)).Returns(product);

        IShopRepo orderService =  orderRepository.Object;


        var actual = orderService.CreateProduct(product);
        Assert.Equal(product, actual);
    }
    
    
}