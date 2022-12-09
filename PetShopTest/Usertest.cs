using Moq;
using PetShop.Application.Interfaces;
using PetShop.Domain;

namespace PetShopTest;

public class Usertest
{
    
    
    
    
    /*[Fact]
    public void DeleteUserById_ValidInput_ReturnsUser()
    {
        // Arrange
        Mock<IUserRepo> userRepositoryMock = new Mock<IUserRepo>();
        var userID = Guid.NewGuid();
        var user = new User { Id = userID, Name = "John Doe" };
        userRepositoryMock.Setup(x => x.DeleteUser(userID)).Returns(user);
       
        
        IUserService userService = new UserService(userRepositoryMock.Object);

        // Act
        var result = userService.DeleteUserById(userID);

        // Assert
        Assert.Equal(user, result);
        userRepositoryMock.Verify(x => x.DeleteUser(userID), Times.Once);
    }*/
}