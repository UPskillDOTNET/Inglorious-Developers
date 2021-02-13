using AutoMapper;
using CentralAPI.Controllers;
using CentralAPI.Data;
using CentralAPI.DTO;
using CentralAPI.Models;
using CentralAPI.Repositories.Repository;
using CentralAPI.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace testCentralAPI
{
    public class ParkingLotsControllerTest
    {
        [Fact]
        public async Task GetAllParkingLotsAsync_ShouldReturnAllParkingLots()
        {
            // Arrange
            var TestContext = CentralAPI_ParkingLotContext.GetCentralAPIContext("GetAllParkingLots");
            var parkingLotRepository = new ParkingLotRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var ParkingLotService = new ParkingLotService(parkingLotRepository, mapper);
            var theController = new ParkingLotsController(ParkingLotService);

            // Act
            var result = await theController.GetParkingLots();


            //Assert
            var items = Assert.IsType<List<ParkingLotDTO>>(result.Value);
            Assert.Equal(2, items.Count);
        }

        [Fact]
        public async Task GetParkingLotByID_ShouldReturnParkingLotByID()
        {
            //Arrange
            var TestContext = CentralAPI_ParkingLotContext.GetCentralAPIContext("GetParkingLotByID");
            var parkingLotRepository = new ParkingLotRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var ParkingLotService = new ParkingLotService(parkingLotRepository, mapper);
            var theController = new ParkingLotsController(ParkingLotService);

            //Act
            var result = await theController.GetParkingLot(1);

            //Assert
            var items = Assert.IsType<ParkingLotDTO>(result.Value);
            Assert.Equal("Parque da República", items.name);
        }

        //public async Task GetGeoAreaAsync_ShouldReturnNotFound()
        //{
        //    // Arrange
        //    var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //    var testContext = OMS_ContextMocker.GetOMSContext(dbName);
        //    var theController = new GeographicalAreaController(testContext);
        //    var testCod = "Z0";

        //    // Act
        //    var response = await theController.GetGeoArea(testCod);

        //    //Assert       
        //    Assert.IsType<NotFoundResult>(response.Result);
        //}

        [Fact]
        public async Task GetParkingLotByID_ShouldReturnNotFound()
        {
            //Arrange
            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            var TestContext = CentralAPI_ParkingLotContext.GetCentralAPIContext(dbName);
            var parkingLotRepository = new ParkingLotRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var ParkingLotService = new ParkingLotService(parkingLotRepository, mapper);
            var theController = new ParkingLotsController(ParkingLotService);

            //Act
            var result = await theController.GetParkingLot(500);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
    }
    public static class CentralAPI_ParkingLotContext
    {
        private static CentralAPIContext dbContext;
        
        public static CentralAPIContext GetCentralAPIContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<CentralAPIContext>()
                            .UseInMemoryDatabase(databaseName: dbName)
                            .Options;

            dbContext = new CentralAPIContext(options);
            Seed();
            return dbContext;
        }

        private static void Seed()
        {
            dbContext.ParkingLots.Add(new ParkingLot { parkingLotID=1, name = "Parque da República", owner = "NorteShopping", location = "Avenida da República", capacity = 125, openingTime = DateTime.Parse("2020-02-22 07:00:00"), closingTime = DateTime.Parse("2999-02-22 19:00:00"), myURL = "https://localhost:44350/api" });
            dbContext.ParkingLots.Add(new ParkingLot { parkingLotID = 2, name = "Parque Brito Capelo", owner = "InRio", location = "Rua Brito Capelo", capacity = 250, openingTime = DateTime.Parse("2020-02-22 07:00:00"), closingTime = DateTime.Parse("2999-02-22 19:00:00"), myURL = "https://localhost:44353/api" });

            dbContext.SaveChanges();
        }
    }
}
