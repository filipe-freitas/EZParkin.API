using EZParkin.API.Domain.Models;
using EZParkin.API.Domain.Repositories;
using EZParkin.API.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EZParkin.API.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        #region Create User

        [TestMethod]
        public async Task CreateAsync_UserWithInvalidEmail_ThrowAnException()
        {
            //Arrange
            var users = new List<User>();

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(setup => setup.CreateAsync(It.IsAny<User>())).ReturnsAsync((User user) =>
            {
                users.Add(user);
                return user;
            });

            var mockUserService = new UserService(mockUserRepository.Object);

            var user = new User
            {
                Name = "John Doe",
                Email = "john.doe.example.com",
                Password = "abc123"
            };

            //Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => mockUserService.CreateAsync(user));
        }

        [TestMethod]
        public async Task CreateAsync_UserWithUsedEmail_ThrowAnException()
        {
            //Arrange
            var users = new List<User>();

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(setup => setup.CreateAsync(It.IsAny<User>())).ReturnsAsync((User user) => {
                users.Add(user);
                return user;
            });
            mockUserRepository.Setup(setup => setup.Get(It.IsAny<string>())).Returns((string userEmail) => {
                var existingUser = users.Where(w => w.Email.ToUpper() == userEmail.ToUpper()).FirstOrDefault() ?? null;
                return existingUser;
            });

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
                Email = "JOHN.DOE@EXAMPLE.COM",
                Password = "123abc",
            };

            await mockUserService.CreateAsync(firstUser);

            //Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(() => mockUserService.CreateAsync(secondUser));
        }

        [TestMethod]
        public async Task CreateAsync_User_ReturnTheUserWithId()
        {
            //Arrange
            var users = new List<User>();

            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(setup => setup.CreateAsync(It.IsAny<User>())).Callback((User user) =>
            {
                users.Add(user);
            }).ReturnsAsync((User user) => user);

            var mockUserService = new UserService(mockRepository.Object);

            var user = new User
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Password = "abc123",
            };

            //Act
            var createdUser = await mockUserService.CreateAsync(user);

            //Assert
            Assert.AreNotEqual(createdUser.Id, user.Id);
            Assert.AreNotEqual(createdUser.CreatedAt, user.CreatedAt);
        }

        [TestMethod]
        public async Task CreateAsync_User_ReturnTheUser()
        {
            //Arrange
            var users = new List<User>();

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(setup => setup.CreateAsync(It.IsAny<User>())).ReturnsAsync((User user) => {
                users.Add(user);
                return user;
            });

            var mockUserService = new UserService(mockUserRepository.Object);

            var user = new User
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Password = "abc123",
            };

            //Act
            var createdUser = await mockUserService.CreateAsync(user);

            //Assert
            Assert.AreEqual(user.Email, createdUser.Email);
        }

        #endregion

        #region Get User

        [TestMethod]
        public async Task Get_ExistingEmail_ReturnTheUser()
        {
            //Arrange
            var users = new List<User>();

            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(setup => setup.CreateAsync(It.IsAny<User>())).ReturnsAsync((User user) => {
                users.Add(user);
                return user;
            });
            mockRepository.Setup(setup => setup.Get(It.IsAny<string>())).Returns((string userEmail) => {
                return users.Where(w => w.Email.ToUpper() == userEmail.ToUpper()).FirstOrDefault() ?? null;
            });

            var mockService = new UserService(mockRepository.Object);

            var user = new User {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Password = "abc123",
            };

            await mockService.CreateAsync(user);

            //Act
            var foundUser = mockService.Get(user.Email);

            //Assert
            Assert.AreEqual(user, foundUser);
        }

        [TestMethod]
        public async Task Get_UnexistingEmail_ReturnNull()
        {
            //Arrange
            var users = new List<User>();

            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(setup => setup.CreateAsync(It.IsAny<User>())).ReturnsAsync((User user) => {
                users.Add(user);
                return user;
            });
            mockRepository.Setup(setup => setup.Get(It.IsAny<string>())).Returns((string userEmail) => {
                return users.Where(w => w.Email.ToUpper() == userEmail.ToUpper()).FirstOrDefault() ?? null;
            });

            var mockService = new UserService(mockRepository.Object);

            var user = new User {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Password = "abc123",
            };

            await mockService.CreateAsync(user);

            //Act
            var foundUser = mockService.Get("jane.doe@example.com");

            //Assert
            Assert.IsNull(foundUser);
        }

        [TestMethod]
        public async Task ListAsync_NoParameters_ContainsUserCreated()
        {
            //Arrange
            var users = new List<User>();

            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(setup => setup.CreateAsync(It.IsAny<User>())).ReturnsAsync((User user) => {
                users.Add(user);
                return user;
            });
            mockRepository.Setup(setup => setup.ListAsync()).ReturnsAsync(users);

            var mockService = new UserService(mockRepository.Object);

            var firstUser = new User
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Password = "abc123",
            };

            var secondUser = new User
            {
                Name = "Jane Doe",
                Email = "jane.doe@example.com",
                Password = "123abc",
            };

            await mockService.CreateAsync(firstUser);
            await mockService.CreateAsync(secondUser);

            //Act
            var listOfUsers = (await mockService.ListAsync()).ToList();

            //Assert
            CollectionAssert.Contains(listOfUsers, firstUser);
            CollectionAssert.Contains(listOfUsers, secondUser);
        }

        #endregion

        #region Update User

        [TestMethod]
        public async Task UpdateAsync_User_UpdatedUser()
        {
            //Arrange
            var users = new List<User>();

            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(setup => setup.UpdateAsync(It.IsAny<User>())).ReturnsAsync((User user) => {
                var userIndex = users.FindIndex(i => i.Id == user.Id);
                if (userIndex != -1) users[userIndex] = user;

                return users.Where(w => w.Id == user.Id).FirstOrDefault();
            });
            mockRepository.Setup(setup => setup.CreateAsync(It.IsAny<User>())).ReturnsAsync((User user) => {
                user.Id = users.LastOrDefault()?.Id + 1 ?? 1;
                users.Add(user);
                return user;
            });
            mockRepository.Setup(setup => setup.Get(It.IsAny<string>())).Returns((string userEmail) => {
                return users.Where(w => w.Email.ToUpper() == userEmail.ToUpper()).FirstOrDefault() ?? null;
            });

            var mockService = new UserService(mockRepository.Object);

            var user = new User
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Password = "abc123",
            };

            await mockService.CreateAsync(user);

            //Act
            var createdUser = mockService.Get(user.Email);
            var updatedUser = await mockService.UpdateAsync(new User
            {
                Id = createdUser.Id,
                Name = "Jane Doe",
                Email = "jane.doe@example.com",
                Password = "123abc",
            });

            //Assert
            Assert.AreEqual(createdUser.Id, updatedUser.Id);
            Assert.AreNotEqual(createdUser.Name, updatedUser.Name);
            Assert.AreNotEqual(createdUser.Email, updatedUser.Email);
            Assert.AreNotEqual(createdUser.Password, updatedUser.Password);
        }

        #endregion

        #region Delete User

        [TestMethod]
        public async Task Delete_UserId_RemoverUserFromListing()
        {
            //Arrange
            var users = new List<User>();

            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(setup => setup.CreateAsync(It.IsAny<User>())).ReturnsAsync((User user) => {
                user.Id = users.LastOrDefault()?.Id + 1 ?? 1;
                users.Add(user);
                return user;
            });
            mockRepository.Setup(setup => setup.Get(It.IsAny<string>())).Returns((string userEmail) => {
                return users.Where(w => w.Email.ToUpper() == userEmail.ToUpper()).FirstOrDefault() ?? null;
            });
            mockRepository.Setup(setup => setup.Delete(It.IsAny<int>())).Callback((int userId) => {
                var userIndex = users.FindIndex(user => user.Id == userId);
                users.RemoveAt(userIndex);
            });

            var mockService = new UserService(mockRepository.Object);

            var firstUser = new User
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Password = "abc123",
            };

            var secondUser = new User
            {
                Name = "Jane Doe",
                Email = "jane.doe@example.com",
                Password = "123abc",
            };

            await mockService.CreateAsync(firstUser);
            await mockService.CreateAsync(secondUser);

            //Act
            mockService.Delete(firstUser.Id);

            //Assert
            CollectionAssert.DoesNotContain(users, firstUser);
            CollectionAssert.Contains(users, secondUser);
        }

        #endregion
    }
}