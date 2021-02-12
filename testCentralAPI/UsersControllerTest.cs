using AutoMapper;
using CentralAPI.Controllers;
using CentralAPI.DTO;
using CentralAPI.Repositories.Repository;
using CentralAPI.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace testCentralAPI {
    public class UsersControllerTest {
        [Fact]
        public async Task GetAllUsersAsync_ShouldReturnAllUsers() {
            // Arrange
            Thread.Sleep(500);
            var TestContext = TodoContextMocker.GetCentralAPIContext("GetAllUsers");
            var userRepository = new UserRepository(TestContext);
            var walletRepository = new WalletRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var UserService = new UserService(userRepository, mapper);
            var WalletService = new WalletService(walletRepository, mapper, userRepository);
            var theController = new UsersController(UserService, WalletService);

            // Act
            var result = await theController.GetAllUsers();

            //Assert
            var items = Assert.IsType<List<UserDTO>>(result.Value);
            Assert.Equal(11, items.Count);
        }

        [Fact]
        public async Task GetUserByID_ShouldReturnUserByID() {
            //Arrange
            Thread.Sleep(500);
            var TestContext = TodoContextMocker.GetCentralAPIContext("GetUserByID");
            var userRepository = new UserRepository(TestContext);
            var walletRepository = new WalletRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var UserService = new UserService(userRepository, mapper);
            var WalletService = new WalletService(walletRepository, mapper, userRepository);
            var theController = new UsersController(UserService, WalletService);

            //Act
            var result = await theController.GetUserById("1");

            //Assert
            var items = Assert.IsType<UserDTO>(result.Value);
            Assert.Equal("Mariana Gomes", items.name);
        }

        [Fact]
        public async Task GetUserByID_ShouldReturnNotFound() {
            //Arrange
            Thread.Sleep(3500);
            var TestContext = TodoContextMocker.GetCentralAPIContext("NotFoundUserByID");
            var userRepository = new UserRepository(TestContext);
            var walletRepository = new WalletRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var UserService = new UserService(userRepository, mapper);
            var WalletService = new WalletService(walletRepository, mapper, userRepository);
            var theController = new UsersController(UserService, WalletService);

            //Act
            var result = await theController.GetUserById("500");

            //Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
    }
}
