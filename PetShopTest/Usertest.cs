using AutoMapper;
using FluentValidation;
using Moq;
using PetShop.Application;
using PetShop.Application.Interfaces;
using PetShop.Application.PostProdDTO;
using PetShop.Domain;

namespace PetShopTest;

public class Usertest
{
    [Fact]
    public void DeleteUserById_ValidInput_ReturnsUser()
    {
        // Arrange
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<Logic> logic = new Mock<Logic>();
        Mock<IValidator<UserDTO>> validator = new Mock<IValidator<UserDTO>>();
        Mock<IUserRepo> userRepositoryMock = new Mock<IUserRepo>();
        var userID = Guid.NewGuid();
        var user = new User { Id = userID, Name = "John Doe" };
        userRepositoryMock.Setup(x => x.DeleteUser(userID)).Returns(user);
       
        
        IUserService userService = new UserService(userRepositoryMock.Object, mapper.Object, validator.Object, logic.Object);

        // Act
        var result = userService.DeleteUserById(userID);

        // Assert
        Assert.Equal(user, result);
        userRepositoryMock.Verify(x => x.DeleteUser(userID), Times.Once);
    }
}