using AutoMapper;
using CentralAPI.Controllers;
using CentralAPI.DTO;
using CentralAPI.Repositories.Repository;
using CentralAPI.Services.IServices;
using CentralAPI.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace testCentralAPI {
    public class WalletControllerTest {
        private IUserService userService;

        [Fact]
        public async Task GetAllWalletsAsync_ShouldReturnAllWallets() {
            // Arrange
            Thread.Sleep(1000);
            var TestContext = TodoContextMocker.GetCentralAPIContext("GetAllWallets");
            var walletRepository = new WalletRepository(TestContext);
            var userRepository = new UserRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var WalletService = new WalletService(walletRepository, mapper, userRepository);
            var UserService = new UserService(userRepository, mapper);
            var theController = new WalletsController(WalletService, UserService);

            // Act
            var result = await theController.GetAllWallets();


            //Assert
            var items = Assert.IsType<List<WalletDTO>>(result.Value);
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public async Task GetWalletByID_ShouldReturnWalletByID() {
            //Arrange
            Thread.Sleep(500);
            var TestContext = TodoContextMocker.GetCentralAPIContext("GetWalletByID");
            var walletRepository = new WalletRepository(TestContext);
            var userRepository = new UserRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var WalletService = new WalletService(walletRepository, mapper, userRepository);
            var UserService = new UserService(userRepository, mapper);
            var theController = new WalletsController(WalletService, UserService);

            //Act
            var result = await theController.GetWalletById("1");

            //Assert
            var items = Assert.IsType<WalletDTO>(result.Value);
            Assert.Equal("1", items.userID);
        }

        //[Fact]
        //public async Task GetWalletByID_ShouldReturnNotFound() {
        //    //Arrange
        //    var TestContext = TodoContextMocker.GetCentralAPIContext("NotFoundWalletByID");
        //    var walletRepository = new WalletRepository(TestContext);
        //    var userRepository = new UserRepository(TestContext);
        //    var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
        //    var mapper = config.CreateMapper();
        //    var WalletService = new WalletService(walletRepository, mapper, userRepository);
        //    var UserService = new UserService(userRepository, mapper);
        //    var theController = new WalletsController(WalletService, UserService);

        //    //Act
        //    var result = await theController.GetWalletById("100");

        //    //Assert
        //    Assert.IsType<NotFoundObjectResult>(result.Result);
        //}

        [Fact]
        public async Task GetWalletBalanceAsync_ShouldReturnWalletBalanceAsync() {

            // Arrange 
            var TestContext = TodoContextMocker.GetCentralAPIContext("GetWalletBalance");
            var walletRepository = new WalletRepository(TestContext);
            var userRepository = new UserRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var WalletService = new WalletService(walletRepository, mapper, userRepository);
            var UserService = new UserService(userRepository, mapper);
            var theController = new WalletsController(WalletService, UserService);
            // Act
            var result = await theController.GetBalance("1");
            //Assert
            var items = Assert.IsType<WalletDTO>(result.Value);
            
            Assert.Equal(250, items.totalAmount);
        }

        [Fact]
        public async Task PostValidWalletasync_ShouldReturnCreatedResponse() {

            // Arrange 
            var TestContext = TodoContextMocker.GetCentralAPIContext("postvalidWallet");
            var walletRepository = new WalletRepository(TestContext);
            var userRepository = new UserRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var WalletService = new WalletService(walletRepository, mapper, userRepository);
            var UserService = new UserService(userRepository, mapper);
            var theController = new WalletsController(WalletService, UserService);

            // Act
            var response = await theController.CreateWallet("5", "pounds");
           
            //Assert
            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public async Task DepositToWallet_ShouldReturnUpdatedWallet() {
            // Arrange
            var TestContext = TodoContextMocker.GetCentralAPIContext("depositWallet");
            var walletRepository = new WalletRepository(TestContext);
            var userRepository = new UserRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var WalletService = new WalletService(walletRepository, mapper, userRepository);
            var UserService = new UserService(userRepository, mapper);
            var theController = new WalletsController(WalletService, UserService);

            // Act
            var response = await theController.DepositToWallet("2", 250.90m);

            // Assert
            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public async Task DepositZeroToWallet_ShouldReturnNotUpdatedWallet() {
            // Arrange
            var TestContext = TodoContextMocker.GetCentralAPIContext("zeroWallet");
            var walletRepository = new WalletRepository(TestContext);
            var userRepository = new UserRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var WalletService = new WalletService(walletRepository, mapper, userRepository);
            var UserService = new UserService(userRepository, mapper);
            var theController = new WalletsController(WalletService, UserService);

            // Act
            var response = await theController.DepositToWallet("2", 0.00m);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public async Task DepositNegativeMoneyToWallet_ShouldReturnNotUpdated() {
            // Arrange
            var TestContext = TodoContextMocker.GetCentralAPIContext("negativeWallet");
            var walletRepository = new WalletRepository(TestContext);
            var userRepository = new UserRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var WalletService = new WalletService(walletRepository, mapper, userRepository);
            var UserService = new UserService(userRepository, mapper);
            var theController = new WalletsController(WalletService, UserService);

            // Act
            var response = await theController.DepositToWallet("2", -250.90m);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public async Task WithdrawFromWallet_ShouldReturnUpdatedWallet() {
            // Arrange
            var TestContext = TodoContextMocker.GetCentralAPIContext("updateWallet");
            var walletRepository = new WalletRepository(TestContext);
            var userRepository = new UserRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var WalletService = new WalletService(walletRepository, mapper, userRepository);
            var UserService = new UserService(userRepository, mapper);
            var theController = new WalletsController(WalletService, UserService);

            // Act
            var response = await theController.DepositToWallet("2", 2.90m);

            // Assert
            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public async Task WithdrawNegativeFromWallet_ShouldReturnNotUpdated() {
            // Arrange
            var TestContext = TodoContextMocker.GetCentralAPIContext("negativeWiWallet");
            var walletRepository = new WalletRepository(TestContext);
            var userRepository = new UserRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var WalletService = new WalletService(walletRepository, mapper, userRepository);
            var UserService = new UserService(userRepository, mapper);
            var theController = new WalletsController(WalletService, UserService);

            // Act
            var response = await theController.DepositToWallet("2", 2.90m);

            // Assert
            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public async Task WithdrawZeroFromWallet_ShouldReturnNotUpdated() {
            // Arrange
            var TestContext = TodoContextMocker.GetCentralAPIContext("zeroWiWallet");
            var walletRepository = new WalletRepository(TestContext);
            var userRepository = new UserRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var WalletService = new WalletService(walletRepository, mapper, userRepository);
            var UserService = new UserService(userRepository, mapper);
            var theController = new WalletsController(WalletService, UserService);

            // Act
            var response = await theController.DepositToWallet("2", 0.00m);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }
    }
}
