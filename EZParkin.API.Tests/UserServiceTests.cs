using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EZParkin.API.Domain.Models;
using EZParkin.API.Domain.Repositories;
using EZParkin.API.Services;

namespace EZParkin.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        #region Create User

        [TestMethod]
        public async Task CreateAnUser()
        {
            //Arrange
            var user = new User
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Password = "abc123",
            };

            var mockUserRepository = new Mock<IUserRepository>();
            var mockUserService = new UserService(mockUserRepository.Object);

            //Act
            var createdUser = await mockUserService.CreateAsync(user);

            //Assert
            Assert.AreEqual(user, createdUser);
        }

        [TestMethod]
        public async Task CreateAnUserWithUsedEmail()
        {
            //Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var mockUserService = new UserService(mockUserRepository.Object);

            var firstUser = new User
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Password = "abc123",
            };

            var secondUser = new User
            {
                Name = "Jane Doe",
                Email = "john.doe@example.com",
                Password = "123abc",
            };

            await mockUserService.CreateAsync(firstUser);

            //Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(() => mockUserService.CreateAsync(secondUser));
        }

        #endregion

        #region Get User

        [TestMethod]
        public async Task GetAnUser()
        {
            throw new System.NotImplementedException();
        }

        [TestMethod]
        public async Task GetAnUnexistingUser()
        {
            throw new System.NotImplementedException();
        }

        [TestMethod]
        public async Task CreatedUserOnList()
        {
            //Arrange
            var user = new User
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Password = "abc123",
            };

            var mockUserRepository = new Mock<IUserRepository>();
            var mockUserService = new UserService(mockUserRepository.Object);

            await mockUserService.CreateAsync(user);

            //Act
            
            var listOfUsers = await mockUserService.ListAsync();

            //Assert
            CollectionAssert.Contains(listOfUsers.ToList(), user);
        }

        #endregion

        #region Update User

        [TestMethod]
        public async Task UpdateAnUser()
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Delete User

        public void DeleteAnUser()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}