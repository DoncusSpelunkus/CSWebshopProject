using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using PetShop.Application;
using PetShop.Application.Interfaces;
using PetShop.Application.PostProdDTO;
using PetShop.Domain;

namespace PetShopTest;

public class Usertest
{
    public class UserServiceTests
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


            IUserService userService =
                new UserService(userRepositoryMock.Object, mapper.Object, validator.Object, logic.Object);

            // Act
            var result = userService.DeleteUserById(userID);

            // Assert
            Assert.Equal(user, result);
            userRepositoryMock.Verify(x => x.DeleteUser(userID), Times.Once);
        }

        [Fact]
        public void GetAllUsers_ReturnsAllUsers()
        {
            // Arrange
            var user1 = new User { Id = Guid.NewGuid(), Name = "John Doe" };
            var user2 = new User { Id = Guid.NewGuid(), Name = "Jane Doe" };
            var expectedUsers = new List<User> { user1, user2 };
            Mock<IMapper> mapper = new Mock<IMapper>();
            Mock<Logic> logic = new Mock<Logic>();
            Mock<IValidator<UserDTO>> validator = new Mock<IValidator<UserDTO>>();
            Mock<IUserRepo> userRepositoryMock = new Mock<IUserRepo>();
            userRepositoryMock.Setup(r => r.GetAllUser()).Returns(expectedUsers);

            var userService = new UserService(userRepositoryMock.Object, mapper.Object, validator.Object, logic.Object);

            // Act
            var users = userService.GetAllUsers();

            // Assert
            Assert.Equal(expectedUsers, users);
            userRepositoryMock.Verify(r => r.GetAllUser(), Times.Once);
        }

        [Fact]
        public void CreateUsers_ValidInput_CreatesUser()
        {
            // Arrange
            Mock<IMapper> mapper = new Mock<IMapper>();
            Mock<Logic> logic = new Mock<Logic>();
            Mock<IValidator<UserDTO>> validator = new Mock<IValidator<UserDTO>>();
            Mock<IUserRepo> userRepositoryMock = new Mock<IUserRepo>();
            var userDTO = new UserDTO { Name = "John Doe", password = "my_password" };
            var user = new User
                { Id = Guid.NewGuid(), Name = "John Doe", HashPassword = new byte[0], SaltPassword = new byte[0] };
            var hashPassword = user.HashPassword;
            var saltPassword = user.SaltPassword;

            mapper.Setup(x => x.Map<User>(userDTO)).Returns(user);
            logic.Setup(x => x.GenerateHash(It.IsAny<string>(), out hashPassword, out saltPassword))
                .Callback((string password, out byte[] hash, out byte[] salt) =>
                {
                    hash = new byte[] { 1, 2, 3 };
                    salt = new byte[] { 4, 5, 6 };
                });
            userRepositoryMock.Setup(x => x.CreateUser(user)).Returns(user);
            validator.Setup(x => x.Validate(userDTO)).Returns(new ValidationResult());
            IUserService userService =
                new UserService(userRepositoryMock.Object, mapper.Object, validator.Object, logic.Object);

            // Act
            var result = userService.CreateUsers(userDTO);

            // Assert
            Assert.Equal(user, result);
            userRepositoryMock.Verify(x => x.CreateUser(user), Times.Once);
            logic.Verify(x => x.GenerateHash(userDTO.password, out hashPassword, out saltPassword), Times.Once);
            Assert.Equal(new byte[] { 1, 2, 3 }, user.HashPassword);
            Assert.Equal(new byte[] { 4, 5, 6 }, user.SaltPassword);
        }
        
        [Fact]
        public void UpdateUser_ValidInput_UpdatesUser()
        {
            // Arrange
            Mock<IMapper> mapper = new Mock<IMapper>();
            Mock<Logic> logic = new Mock<Logic>();
            Mock<IValidator<UserDTO>> validator = new Mock<IValidator<UserDTO>>();
            Mock<IUserRepo> userRepositoryMock = new Mock<IUserRepo>();
            var userID = Guid.NewGuid();
            var user = new User { Id = userID, Name = "John Doe" };
            var userDTO = new UserDTO { Name = "Jane Doe", password = "new_password" };
            var hashPassword = user.HashPassword;
            var saltPassword = user.SaltPassword;
            mapper.Setup(x => x.Map(userDTO, user)).Callback(() =>
            {
                user.Name = userDTO.Name;
            });
            logic.Setup(x => x.GenerateHash(userDTO.password, out hashPassword, out saltPassword))
                .Callback((string password, out byte[] hash, out byte[] salt) =>
                {
                    hash = new byte[] { 1, 2, 3 };
                    salt = new byte[] { 4, 5, 6 };
                });
            userRepositoryMock.Setup(x => x.UpdateUser(user)).Returns(user);
            validator.Setup(x => x.Validate(userDTO)).Returns(new ValidationResult());
            IUserService userService = new UserService(userRepositoryMock.Object, mapper.Object, validator.Object, logic.Object);

            // Act
            var result = userService.UpdateUser(user, userDTO);

            // Assert
            Assert.Equal(user, result);
            userRepositoryMock.Verify(x => x.UpdateUser(user), Times.Once);
            logic.Verify(x => x.GenerateHash(userDTO.password, out hashPassword, out saltPassword), Times.Once);
            Assert.Equal(new byte[] { 1, 2, 3 }, user.HashPassword);
            Assert.Equal(new byte[] { 4, 5, 6 }, user.SaltPassword);
            Assert.Equal("Jane Doe", user.Name);
        }
    }
}