using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using CentralAPI.DTO;
using CentralAPI.Repositories.Repository;
using AutoMapper;
using CentralAPI.Controllers;
using CentralAPI.Services.Services;
using CentralAPI.Services.IServices;

namespace testCentralAPI
{
    public class UserTest
    {
        [Fact]
        public async Task GetAllUsersAsync_ShouldReturnAllUsers()
        {
            // Arrange
            Thread.Sleep(500);
            var TestContext = TodoContextMocker.GetCentralAPIContext("GetAllUsers");
            var userRepository = new UserRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var UserService = new UserService(userRepository, mapper);
            var theController = new UsersController(UserService);

            // Act
            var result = await theController.GetAllUsers();


            //Assert
            var items = Assert.IsType<List<UserDTO>>(result.Value);
            Assert.Equal(11, items.Count);
        }

        [Fact]
        public async Task GetUserByID_ShouldReturnUserByID()
        {
            //Arrange
            Thread.Sleep(500);
            var TestContext = TodoContextMocker.GetCentralAPIContext("GetUserByID");
            var userRepository = new UserRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var UserService = new UserService(userRepository, mapper);
            var theController = new UsersController(UserService);

            //Act
            var result = await theController.GetUserById("1");

            //Assert
            var items = Assert.IsType<UserDTO>(result.Value);
            Assert.Equal("Mariana Gomes", items.name);
        }
    }
}
