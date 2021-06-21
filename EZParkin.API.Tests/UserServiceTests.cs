using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using EZParkin.API.Domain.Models;
using EZParkin.API.Domain.Services;
using EZParkin.API.Domain.Repositories;
using EZParkin.API.Services;
using System.Threading.Tasks;

namespace EZParkin.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        #region Create User

        [TestMethod]
        public async Task UserCreatedReturnUser()
        {
            //Arrange
            var user = new User
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Password = "abc123",
            };

            var mockUserRepository = new Mock<IUserRepository>();

            //Act
            var mockUserService = new UserService(mockUserRepository.Object);
            var createdUser = await mockUserService.CreateAsync(user);

            //Assert
            Assert.AreEqual(user, createdUser);
        }

        [TestMethod]
        public async Task UserCreatedIsListed()
        {
            //Arrange
            var user = new User
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Password = "abc123",
            };

            var mockUserRepository = new Mock<IUserRepository>();
            //mockUserRepository.Setup(setup => setup.CreateAsync(user));

            //Act
            var mockUserService = new UserService(mockUserRepository.Object);
            var listOfUsers = await mockUserService.ListAsync();

            System.Console.WriteLine(listOfUsers);

            //Assert
            CollectionAssert.Contains(listOfUsers.ToList(), user);
        }

        #endregion
    }
}