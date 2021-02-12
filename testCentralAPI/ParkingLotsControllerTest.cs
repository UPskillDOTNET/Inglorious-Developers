using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using CentralAPI.DTO;
using CentralAPI.Data;
using CentralAPI.Repositories.Repository;
using AutoMapper;
using CentralAPI.Controllers;
using CentralAPI.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace testCentralAPI {
    public class ParkingLotsControllerTest {
        [Fact]
        public async Task GetAllParkingLotsAsync_ShouldReturnAllParkingLots() {
            // Arrange
            Thread.Sleep(1000);
            var TestContext = TodoContextMocker.GetCentralAPIContext("GetAllParkingLots");
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
        public async Task GetParkingLotByID_ShouldReturnParkingLotByID() {
            //Arrange
            Thread.Sleep(1000);
            var TestContext = TodoContextMocker.GetCentralAPIContext("GetParkingLot");
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

        [Fact]
        public async Task GetParkingLotByID_ShouldReturnNotFound() {
            //Arrange
            Thread.Sleep(500);
            var TestContext = TodoContextMocker.GetCentralAPIContext("GetParkingLotByID");
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
}
