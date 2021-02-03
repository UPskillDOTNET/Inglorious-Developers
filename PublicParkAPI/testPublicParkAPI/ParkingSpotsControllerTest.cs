using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using PublicParkAPI.Models;
using testProject;
using PublicParkAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using PublicParkAPI.Repositories;
using AutoMapper;
using PublicParkAPI.Mappings;
using PublicParkAPI.Services.Services;
using PublicParkAPI.DTO;

namespace testPublicParkAPI
{
    public class ParkingSpotsControllerTest
    {

        [Fact]
        public async Task GetAllParkingSpotsAsync_ShouldReturnAllParkingSpotsAsync()
        {
            // Arrange            
            var testContext = TodoContextMocker.GetPublicParkContext("GetAllParkingSpots");
            var parkingSpotRepository = new ParkingSpotRepository(testContext);
            var reservationRepository = new ReservationRepository(testContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, mapper, reservationRepository);
            var theController = new ParkingSpotsController(parkingSpotService);

            // Act
            var result = await theController.GetAllParkingSpots();

            // Assert
            var parkingSpots = Assert.IsType<List<ParkingSpotDTO>>(result.Value);
            Assert.Equal(5, parkingSpots.Count());
        }


        [Fact]
        public async Task GetParkingSpotAsync_ShouldReturnNotFound()
        {
            Thread.Sleep(300);
            // Arrange
            var TestContext = TodoContextMocker.GetPublicParkContext("GetParkingSpotNotFound");
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var reservationRepository = new ReservationRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, mapper, reservationRepository);
            var theController = new ParkingSpotsController(parkingSpotService);
            var testCod = "E21";

            // Act
            var response = await theController.GetParkingSpot(testCod);

            // Assert
            Assert.IsType<NotFoundObjectResult>(response.Result);
        }

        [Fact]
        public async Task GetParkingSpotAsync_ShouldReturnParkingSpot()
        {
            Thread.Sleep(3000);
            // Arrange
            var TestContext = TodoContextMocker.GetPublicParkContext("GetSpecificParkingSpot");
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var reservationRepository = new ReservationRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, mapper, reservationRepository);
            var theController = new ParkingSpotsController(parkingSpotService);
            var testCod = "E1";

            // Act
            var response = await theController.GetParkingSpot(testCod);

            // Assert
            Assert.IsType<ParkingSpotDTO>(response.Value);
        }

        [Fact]
        public async Task GetParkingSpotAsync_ShouldReturnTheRightItemAsync()
        {
            Thread.Sleep(300);
            // Arrange
            var TestContext = TodoContextMocker.GetPublicParkContext("GetRightParkingSpot");
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var reservationRepository = new ReservationRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, mapper, reservationRepository);
            var theController = new ParkingSpotsController(parkingSpotService);
            var testCod = "E1";

            // Act
            var response = await theController.GetParkingSpot(testCod);
            var parkingSpotItem = response.Value;

            // Assert
            Assert.IsType<ParkingSpotDTO>(parkingSpotItem);
            Assert.Equal(testCod, parkingSpotItem.parkingSpotID);
        }

        [Fact]
        public async Task PostNoPriceParkingSpotAsync_ShouldReturnBadRequest()
        {
            Thread.Sleep(400);
            // Arrange
            var TestContext = TodoContextMocker.GetPublicParkContext("PostbadParkingSpot");
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var reservationRepository = new ReservationRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, mapper, reservationRepository);
            var theController = new ParkingSpotsController(parkingSpotService); 
            var noPriceParkingSpot = new ParkingSpotDTO
            {
                parkingSpotID = "T1",
                ParkingLotID = 1
            };

            // Act
            var response = await theController.PostParkingSpot(noPriceParkingSpot);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task PostNoParkingLotIDParkingSpotAsync_ShouldReturnBadRequest()
        {
            Thread.Sleep(3100);
            // Arrange
            var TestContext = TodoContextMocker.GetPublicParkContext("PostNoIdParkingSpot");
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var reservationRepository = new ReservationRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, mapper, reservationRepository);
            var theController = new ParkingSpotsController(parkingSpotService);
            var noParkingLotID = new ParkingSpotDTO
            {
                parkingSpotID = "F1",
                priceHour = 5
            };

            theController.ModelState.AddModelError("parkingLotID", "Required");

            // Act
            var response = await theController.PostParkingSpot(noParkingLotID);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task PostParkingSpotAsync_ShouldReturnCreatedResponse()
        {
            Thread.Sleep(300);
            // Arrange
            var TestContext = TodoContextMocker.GetPublicParkContext("PostParkingSpotReturnCreatedResponse");
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var reservationRepository = new ReservationRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, mapper, reservationRepository);
            var theController = new ParkingSpotsController(parkingSpotService);
            var theNewParkingSpot = new ParkingSpotDTO
            {
                parkingSpotID = "T13",
                priceHour = 0.9m,
                ParkingLotID = 2
            };

                // Act
                var response = await theController.PostParkingSpot(theNewParkingSpot);

                // Assert
                Assert.IsType<CreatedAtActionResult>(response);
            }

        [Fact]
        public async Task PostParkingSpotAsync_ShouldCreateAnParkingSpotAsync()
        {
            Thread.Sleep(500);
            // Arrange
            var TestContext = TodoContextMocker.GetPublicParkContext("PostParkingSpotCreateParkingSpot");
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var reservationRepository = new ReservationRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, mapper, reservationRepository);
            var theController = new ParkingSpotsController(parkingSpotService);
            var thenewParkingSpot = new ParkingSpotDTO
            {
                parkingSpotID = "T435",
                priceHour = 0.9m,
                ParkingLotID = 2
            };

            // Act
            var response = await theController.PostParkingSpot(thenewParkingSpot);
            var result = response as CreatedAtActionResult;

            // Assert
            Assert.NotNull(response);
            Assert.IsNotType<BadRequestObjectResult>(result);
            Assert.IsType<ParkingSpotDTO>(result.Value);

            var theParkingSpot = result.Value as ParkingSpotDTO;
            Assert.Equal("T435", theParkingSpot.parkingSpotID);
        }

        [Fact]
        public async Task PutNoExistingParkingSpotAsync_ShouldReturnNotFound()
        {
            Thread.Sleep(300);
            // Arrange
            var TestContext = TodoContextMocker.GetPublicParkContext("PutNotParkingSpotReturnNotFound");
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var reservationRepository = new ReservationRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, mapper, reservationRepository);
            var theController = new ParkingSpotsController(parkingSpotService);
            var testCod = "NoExCod";

            var theNonParkingSpot = new ParkingSpotDTO
            {
                parkingSpotID = testCod,
                priceHour = 0.9m,
                ParkingLotID = 2
            };

            // Act
            var response = await theController.PutParkingSpot(testCod, theNonParkingSpot);

            // Assert
            Assert.IsType<NotFoundObjectResult>(response);
        }

        [Fact]
        public async Task PutNoPriceParkingSpot_ShouldReturnBadRequestResult()
        {
            Thread.Sleep(400);
            // Arrange
            var TestContext = TodoContextMocker.GetPublicParkContext("PutNoPriceParkingSpotReturnBadRequest");
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var reservationRepository = new ReservationRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, mapper, reservationRepository);
            var theController = new ParkingSpotsController(parkingSpotService);
            var testCod = "A1";

            var noPriceParkingSpot = new ParkingSpotDTO
            {
                parkingSpotID = testCod,
                ParkingLotID = 1
            };            

            // Act
            var response = await theController.PutParkingSpot(testCod, noPriceParkingSpot);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);

        }

        [Fact]
        public async Task PutNoParkingLotIDParkingSpot_ShouldReturnBadRequest()
        {
            Thread.Sleep(300);
            // Arrange
            var TestContext = TodoContextMocker.GetPublicParkContext("PutNoParkingLotIDParkingSpotReturnBadRequest");
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var reservationRepository = new ReservationRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, mapper, reservationRepository);
            var theController = new ParkingSpotsController(parkingSpotService);
            var testCod = "A1";

            var noPriceParkingSpot = new ParkingSpotDTO
            {
                parkingSpotID = testCod,
                priceHour = 0.9m,
            };

            // Act
            var response = await theController.PutParkingSpot(testCod, noPriceParkingSpot);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task PutParkingSpot_ShouldReturnNoContentResult()
        {
            Thread.Sleep(1600);
            // Arrange
            var TestContext = TodoContextMocker.GetPublicParkContext("PutParkingSpotReturnNoContentResult");
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var reservationRepository = new ReservationRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, mapper, reservationRepository);
            var theController = new ParkingSpotsController(parkingSpotService);
            var testCod = "O1";
            var theParkingSpot = new ParkingSpotDTO
            {
                parkingSpotID = testCod,
                priceHour = 0.30m,
                ParkingLotID = 2
            };

            var c = await TestContext.FindAsync<ParkingSpot>(testCod); //To Avoid tracking error
            TestContext.Entry(c).State = EntityState.Detached;

            // Act
            var response = await theController.PutParkingSpot(testCod, theParkingSpot);
            var getResult = await theController.GetParkingSpot(theParkingSpot.parkingSpotID);

            // Assert
            var items = Assert.IsType<ParkingSpotDTO>(getResult.Value);
            Assert.Equal(0.30m, items.priceHour);
            Assert.IsType<NoContentResult>(response);
        }

        //[Fact]
        //public async Task DeleteNotExistingParkingSpot_ShouldReturnNotFound()
        //{
        //    Thread.Sleep(300);
        //    // Arrange
        //    var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //    var testContext = TodoContextMocker.GetPublicParkContext(dbName);
        //    var theController = new ParkingSpotsController(testContext);
        //    var testCod = "NoExCod";

        //    // Act
        //    var result = await theController.DeleteParkingSpot(testCod);

        //    // Assert
        //    Assert.IsType<NotFoundResult>(result);
        //}

        //[Fact]
        //public async Task DeleteExistingParkingSpot_ShouldRemovetheCountryAsync()
        //{
        //    // Arrange
        //    var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //    var testContext = TodoContextMocker.GetPublicParkContext(dbName);
        //    var theController = new ParkingSpotsController(testContext);
        //    var testCod = "A1";

        //    // Act
        //    var result = await theController.DeleteParkingSpot(testCod);

        //    var isTheItemThere = await theController.GetParkingSpot(testCod);

        //    // Assert
        //    Assert.IsType<NotFoundResult>(isTheItemThere.Result);
    }
        //[Fact]
        //public async Task DeleteNotExistingCountry_ShouldReturnNotFound()
        //{
        //    // Arrange
        //    var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //    var testContext = MockerOMSContext.GetTheOMSContext(dbName);
        //    var theController = new CountryController(testContext);
        //    var testCod = "NoExCod";

        //    // Act
        //    var result = await theController.DeleteCountry(testCod);

        //    // Assert
        //    Assert.IsType<NotFoundResult>(result);
        //}
    }



