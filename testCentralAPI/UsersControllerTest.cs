using AutoMapper;
using CentralAPI.Controllers;
using CentralAPI.Data;
using CentralAPI.DTO;
using CentralAPI.Models;
using CentralAPI.Repositories.Repository;
using CentralAPI.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace testCentralAPI {
    public class UsersControllerTest {
        [Fact]
        public async Task GetAllUsersAsync_ShouldReturnAllUsers() {
            // Arrange
            var TestContext = CentralAPI_UserContext.GetCentralAPIContext("GetAllUsers");
            var userRepository = new UserRepository(TestContext);
            var walletRepository = new WalletRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var WalletService = new WalletService(walletRepository, mapper, userRepository);
            var UserService = new UserService(userRepository, WalletService, mapper);
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
            var TestContext = CentralAPI_UserContext.GetCentralAPIContext("GetUserByID");
            var userRepository = new UserRepository(TestContext);
            var walletRepository = new WalletRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var WalletService = new WalletService(walletRepository, mapper, userRepository);
            var UserService = new UserService(userRepository, WalletService, mapper);
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
            var TestContext = CentralAPI_UserContext.GetCentralAPIContext("NotFoundUserByID");
            var userRepository = new UserRepository(TestContext);
            var walletRepository = new WalletRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var WalletService = new WalletService(walletRepository, mapper, userRepository);
            var UserService = new UserService(userRepository, WalletService, mapper);
            var theController = new UsersController(UserService, WalletService);

            //Act
            var result = await theController.GetUserById("500");

            //Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
    }
    public static class CentralAPI_UserContext
    {
        private static CentralAPIContext userContext;

        public static CentralAPIContext GetCentralAPIContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<CentralAPIContext>()
                            .UseInMemoryDatabase(databaseName: dbName)
                            .Options;

            userContext = new CentralAPIContext(options);
            Seed();
            return userContext;
        }

        private static void Seed()
        {
            userContext.Users.Add(new User { userID = "1", name = "Mariana Gomes", email = "marianagomes@gmail.com", nif = "221133486" });
            userContext.Users.Add(new User { userID = "2", name = "Tiago Azevedo", email = "tiagoazevedo@gmail.com", nif = "113333986" });
            userContext.Users.Add(new User { userID = "3", name = "João Martins", email = "joaomartins@gmail.com", nif = "231163886" });
            userContext.Users.Add(new User { userID = "4", name = "Diego Maradona", email = "dieguito@gmail.com", nif = "554112892" });
            userContext.Users.Add(new User { userID = "5", name = "Michael Jordan", email = "air.jordan@gmail.com", nif = "554112870" });
            userContext.Users.Add(new User { userID = "6", name = "Freddie Mercury", email = "music@gmail.com", nif = "553112870" });
            userContext.Users.Add(new User { userID = "7", name = "Queen Elizabeth II", email = "the.queen@gmail.com", nif = "554112881" });
            userContext.Users.Add(new User { userID = "8", name = "Afonso Henriques", email = "afonso.rei@gmail.com", nif = "554112894" });
            userContext.Users.Add(new User { userID = "9", name = "Elon Musk", email = "spaceX@gmail.com", nif = "583112870" });
            userContext.Users.Add(new User { userID = "10", name = "André André", email = "vitoriasc@gmail.com", nif = "554112881" });
            userContext.Users.Add(new User { userID = "11", name = "Jô Soares", email = "gordo@gmail.com", nif = "154112894" });

            userContext.SaveChanges();
        }
    }
}
