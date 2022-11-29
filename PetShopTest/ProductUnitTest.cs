using PetShop.Application.Interfaces;
using PetShop.Domain;
using Moq;

namespace PetShopTest;

public class UnitTest1
{
    [Fact]
    public void GetProductByIDTest_ValidData()
    {
        Mock<IShopRepo> orderRepository = new Mock<IShopRepo>();
        int id = 1;
        Product expected = new Product()
        {
            ID = id, Name = "mockFood1", Price = 10, Description = "very good product1", ImageUrl = "fakeURL1",
            Rating = 1.5, MainCategory = 0, SubCategory = 0, Brand = 0
        };
        orderRepository.Setup(repo => repo.GetProductByID(id)).Returns(expected);
        
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
    public void DeleteByID_NotValidData()
    {

        Mock<IShopRepo> orderRepository = new Mock<IShopRepo>();
        int Id = 1;
        Product product = new Product()
        {
            ID = 2, Name = "mockFood1", Price = 10, Description = "very good product1", ImageUrl = "fakeURL1",
            Rating = 1.5, MainCategory = 0, SubCategory = 0, Brand = 0
        };


        orderRepository.Setup(repo => repo.DeleteProduct(Id)).Returns(product);

        IShopRepo orderService =  orderRepository.Object;


        var actual = orderService.DeleteProduct(2);
        Assert.NotEqual(product, actual);
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