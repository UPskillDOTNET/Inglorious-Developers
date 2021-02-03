using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrivateParkAPI.Controllers;
using PrivateParkAPI.Models;
using PrivateParkAPI.Repositories.Repository;
using PrivateParkAPI.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using testProject;
using Xunit;
using PrivateParkAPI.DTO;

namespace testPrivateParkAPI
{
    public class ParkingLotsControllerTest
    {
        [Fact]
        public async Task GetAllParkingLotsAsync_ShouldReturnAllParkingLots()
        {
            // Arrange
            Thread.Sleep(1000);
            var TestContext = TodoContextMocker.GetPrivateParkContext("GetAllParkingLots");
            var parkingLotRepository = new ParkingLotRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var ParkingLotService = new ParkingLotService(parkingLotRepository, mapper);
            var theController = new ParkingLotsController(ParkingLotService);

            // Act
            var result = await theController.GetParkingLots();


            //Assert
            var items = Assert.IsType<List<ParkingLotDTO>>(result.Value);
            Assert.Equal(5, items.Count);
        }

        [Fact]
        public async Task GetParkingLotByID_ShouldReturnParkingLotByID()
        {
            //Arrange
            Thread.Sleep(1000);
            var TestContext = TodoContextMocker.GetPrivateParkContext("GetParkingLot");
            var parkingLotRepository = new ParkingLotRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var ParkingLotService = new ParkingLotService(parkingLotRepository, mapper);
            var theController = new ParkingLotsController(ParkingLotService);

            //Act
            var result = await theController.GetParkingLot(3);

            //Assert
            var items = Assert.IsType<ParkingLotDTO>(result.Value);
            Assert.Equal("Parque da Liberdade", items.name);
        }

        [Fact]
        public async Task GetParkingLotByID_ShouldReturnNotFound()
        {
            //Arrange
            Thread.Sleep(1000);
            var TestContext = TodoContextMocker.GetPrivateParkContext("NotFoundParkingLotByID");
            var parkingLotRepository = new ParkingLotRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var ParkingLotService = new ParkingLotService(parkingLotRepository, mapper);
            var theController = new ParkingLotsController(ParkingLotService);

            //Act
            var result = await theController.GetParkingLot(1231231213);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        //[Fact]
        //public async Task PutParkingLot_ShouldReturnEditedParkingLot()
        //{
        //     Arrange
        //    Thread.Sleep(1000);
        //    var TestContext = TodoContextMocker.GetPrivateParkContext("PutParkingLot");
        //    var theController = new ParkingLotsController(TestContext);

        //    var newParkingLot = new ParkingLot
        //    {
        //        parkingLotID = 1,
        //        name = "Parque da República",
        //        companyOwner = "NorteShopping",
        //        location = "Avenida da República",
        //        capacity = 140,
        //        openingTime = DateTime.Parse("2020-02-22 07:00:00"),
        //        closingTime = DateTime.Parse("2999-02-22 19:00:00")
        //    };

        //    var oldParkingLotResult = await theController.GetParkingLot(1);
        //    var oldParkingLot = oldParkingLotResult.Value;
        //    TestContext.Entry(oldParkingLot).State = EntityState.Detached;

        //     Act
        //    var result = await theController.PutParkingLot(newParkingLot.parkingLotID, newParkingLot);
        //    var getResult = await theController.GetParkingLot(newParkingLot.parkingLotID);

        //    Assert
        //    var items = Assert.IsType<ParkingLot>(getResult.Value);
        //    Assert.Equal(140, items.capacity);
        //    Assert.IsType<NoContentResult>(result);
        //}

        //[Fact]
        //public async Task PutNoParkingLot_ShouldReturnNotFound()
        //{
        //     Arrange
        //    Thread.Sleep(1000);
        //    var TestContext = TodoContextMocker.GetPrivateParkContext("NotPutParkingLot");
        //    var theController = new ParkingLotsController(TestContext);

        //    var newParkingLot = new ParkingLot
        //    {
        //        parkingLotID = 0,
        //        name = "Jardim da Pérgola",
        //        companyOwner = "Porto Beach Apartment",
        //        location = "Avenida do Brasil",
        //        capacity = 150,
        //        openingTime = DateTime.Parse("2020-02-22 07:00:00"),
        //        closingTime = DateTime.Parse("2999-02-22 19:00:00")
        //    };

        //    var oldParkingLotResult = await theController.GetParkingLot(1);
        //    var oldParkingLot = oldParkingLotResult.Value;
        //    TestContext.Entry(oldParkingLot).State = EntityState.Detached;

        //     Act
        //    var getResult = await theController.GetParkingLot(newParkingLot.parkingLotID);

        //    Assert
        //    Assert.IsType<NotFoundObjectResult>(getResult.Result);
        //}

        //[Fact]
        //public async Task PutNoParkingLot_ShouldReturnBadRequest()
        //{
        //     Arrange
        //    Thread.Sleep(1000);
        //    var TestContext = TodoContextMocker.GetPrivateParkContext("PutBadRequesParkingLot");
        //    var theController = new ParkingLotsController(TestContext);

        //    var newParkingLot = new ParkingLot
        //    {
        //        parkingLotID = 1,
        //        companyOwner = "NorteShopping",
        //        closingTime = DateTime.Parse("2999-02-22 19:00:00")
        //    };
        //    theController.ModelState.AddModelError("name", "Required");

        //    var oldParkingLotResult = await TestContext.FindAsync<ParkingLot>(1);
        //    TestContext.Entry(oldParkingLotResult).State = EntityState.Detached;


        //     Act
        //    var getResult = await theController.PutParkingLot(1, newParkingLot);

        //    Assert
        //    Assert.IsType<BadRequestResult>(getResult);
        //}

        [Fact]
        public async Task PostParkingLot_ShouldCreateNewParkingLot()
        {
            //Arrange
            Thread.Sleep(1000);
            var TestContext = TodoContextMocker.GetPrivateParkContext("PostParkingLot");
            var parkingLotRepository = new ParkingLotRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var ParkingLotService = new ParkingLotService(parkingLotRepository, mapper);
            var theController = new ParkingLotsController(ParkingLotService);

            var newParkingLot = new ParkingLotDTO
            {
                parkingLotID = 6,
                name = "Jardim da Pérgola",
                companyOwner = "Porto Beach Apartment",
                location = "Avenida do Brasil",
                capacity = 150,
                openingTime = DateTime.Parse("2020-02-22 07:00:00"),
                closingTime = DateTime.Parse("2999-02-22 19:00:00")
            };

            //Act
            var result = await theController.PostParkingLot(newParkingLot);
            var getResult = await theController.GetParkingLot(6);

            //Assert
            var items = Assert.IsType<ParkingLotDTO>(getResult.Value);
            Assert.Equal("Jardim da Pérgola", items.name);
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task PostBadParkingLot_ShouldReturnBadRequest()
        {
            //Arrange
            Thread.Sleep(1000);

            var TestContext = TodoContextMocker.GetPrivateParkContext("PostBadRequestParkingLot");
            var parkingLotRepository = new ParkingLotRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var ParkingLotService = new ParkingLotService(parkingLotRepository, mapper);
            var theController = new ParkingLotsController(ParkingLotService);

            var newParkingLot = new ParkingLotDTO
            {
                parkingLotID = 7,
                openingTime = DateTime.Parse("2020-02-22 07:00:00"),
                closingTime = DateTime.Parse("2999-02-22 19:00:00")
            };
            theController.ModelState.AddModelError("name", "Required");

            //Act
            var result = await theController.PostParkingLot(newParkingLot);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        //[Fact]
        //public async Task DeleteParkingLot_ShouldDeleteParkingLot()
        //{
        //     Arrange
        //    Thread.Sleep(1500);
        //    var TestContext = TodoContextMocker.GetPrivateParkContext("DeleteParkingLot");
        //    var theController = new ParkingLotsController(TestContext);

        //     Act
        //    var result = await theController.DeleteParkingLot(5);

        //    Assert
        //    Assert.IsType<NoContentResult>(result);
        //}

        //[Fact]
        //public async Task DeleteNotExistParkingLot_ShouldReturnNotFound()
        //{
        //     Arrange
        //    Thread.Sleep(1600);
        //    var TestContext = TodoContextMocker.GetPrivateParkContext("NotFoundDeleteParkingLot");
        //    var theController = new ParkingLotsController(TestContext);

        //     Act
        //    var result = await theController.DeleteParkingLot(10);

        //    Assert
        //    Assert.IsType<NotFoundObjectResult>(result);
        //}
    }
}

