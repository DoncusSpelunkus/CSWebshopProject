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
     List<Product> fakeRepo = new List<Product>();
        
           fakeRepo.Add(new Product() { ID = 1, Name = "mockFood1", Price = 10, Description = "very good product1", ImageUrl = "fakeURL1", Rating = 1.5, MainCategory = 0, SubCategory = 0, Brand = 0});
           fakeRepo.Add( new Product() { ID = 2, Name = "mockFood2", Price = 20, Description = "very good product2", ImageUrl = "fakeURL2", Rating = 2.5, MainCategory = 0, SubCategory = 0, Brand = 0});
           fakeRepo.Add( new Product() { ID = 3, Name = "mockFood3", Price = 30, Description = "very good product3", ImageUrl = "fakeURL3", Rating = 3.5, MainCategory = 0, SubCategory = 0, Brand = 0});
        
        Mock<IShopRepo> mockRepository = new Mock<IShopRepo>();
        mockRepository.Setup(r=>r.GetAllProducts()).Returns(fakeRepo);
    
        
        IShopService service = new ShopService(mockRepository.Object, null, null );
        
        Product expectedValue = new Product() { ID = 1, Name = "mockFood1", Price = 10, Description = "very good product1", ImageUrl = "fakeURL1", Rating = 1.5, MainCategory = 0, SubCategory = 0, Brand = 0};
        
        // Act
        Product result = service.GetProductByID(1);
        // Assert
        Assert.Equal(expectedValue, result );
    
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