using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicParkAPI.Controllers;
using PublicParkAPI.Data;
using PublicParkAPI.DTO;
using PublicParkAPI.Mappings;
using PublicParkAPI.Models;
using PublicParkAPI.Repositories;
using PublicParkAPI.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace testPublicParkAPI
{
    public class ParkingSpotsControllerTest
    {

        [Fact]
        public async Task GetAllParkingSpotsAsync_ShouldReturnAllParkingSpotsAsync()
        {
            // Arrange            
            var testContext = PublicPark_ParkingSpotsContext.GetPublicParkContext("GetAllParkingSpots");
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
            // Arrange
            var TestContext = PublicPark_ParkingSpotsContext.GetPublicParkContext("GetParkingSpotNotFound");
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
            // Arrange
            var TestContext = PublicPark_ParkingSpotsContext.GetPublicParkContext("GetSpecificParkingSpot");
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
            // Arrange
            var TestContext = PublicPark_ParkingSpotsContext.GetPublicParkContext("GetRightParkingSpot");
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
            // Arrange
            var TestContext = PublicPark_ParkingSpotsContext.GetPublicParkContext("PostbadParkingSpot");
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var reservationRepository = new ReservationRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, mapper, reservationRepository);
            var theController = new ParkingSpotsController(parkingSpotService);
            var noPriceParkingSpot = new ParkingSpotDTO
            {
                parkingSpotID = "T1",
            };

            // Act
            var response = await theController.PostParkingSpot(noPriceParkingSpot);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public async Task PostNoParkingLotIDParkingSpotAsync_ShouldReturnBadRequest()
        {
            // Arrange
            var TestContext = PublicPark_ParkingSpotsContext.GetPublicParkContext("PostNoIdParkingSpot");
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
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public async Task PostParkingSpotAsync_ShouldReturnCreatedResponse()
        {
            // Arrange

            var TestContext = PublicPark_ParkingSpotsContext.GetPublicParkContext("PostParkingSpotReturnCreatedResponse");
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
            };

            // Act
            var response = await theController.PostParkingSpot(theNewParkingSpot);

            // Assert
            Assert.IsType<CreatedAtActionResult>(response.Result);
        }

        [Fact]
        public async Task PostParkingSpotAsync_ShouldCreateAnParkingSpotAsync()
        {
        
            // Arrange
            var TestContext = PublicPark_ParkingSpotsContext.GetPublicParkContext("PostParkingSpotCreateParkingSpot");
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
            };

            // Act
            var response = await theController.PostParkingSpot(thenewParkingSpot);
            var result = await theController.GetParkingSpot("T435");

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
            // Arrange
            var TestContext = PublicPark_ParkingSpotsContext.GetPublicParkContext("PutNotParkingSpotReturnNotFound");
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
            };

            // Act
            var response = await theController.PutParkingSpot(testCod, theNonParkingSpot);

            // Assert
            Assert.IsType<NotFoundObjectResult>(response.Result);
        }

        [Fact]
        public async Task PutNoPriceParkingSpot_ShouldReturnBadRequestResult()
        {
            // Arrange
            var TestContext = PublicPark_ParkingSpotsContext.GetPublicParkContext("PutNoPriceParkingSpotReturnBadRequest");
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var reservationRepository = new ReservationRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, mapper, reservationRepository);
            var theController = new ParkingSpotsController(parkingSpotService);
            var testCod = "A1";

            var noPriceParkingSpot = new ParkingSpotDTO
            {
                parkingSpotID = testCod
            };

            // Act
            var response = await theController.PutParkingSpot(testCod, noPriceParkingSpot);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response.Result);

        }

        [Fact]
        public async Task PutNoParkingLotIDParkingSpot_ShouldReturnBadRequest()
        {
            // Arrange
            var TestContext = PublicPark_ParkingSpotsContext.GetPublicParkContext("PutNoParkingLotIDParkingSpotReturnBadRequest");
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
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public async Task PutParkingSpot_ShouldReturnNoContentResult()
        {
            // Arrange
            var TestContext = PublicPark_ParkingSpotsContext.GetPublicParkContext("PutParkingSpotReturnNoContentResult");
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
                priceHour = 0.30m
            };

            var c = await TestContext.FindAsync<ParkingSpot>(testCod); //To Avoid tracking error
            TestContext.Entry(c).State = EntityState.Detached;

            // Act
            var response = await theController.PutParkingSpot(testCod, theParkingSpot);
            var getResult = await theController.GetParkingSpot(theParkingSpot.parkingSpotID);

            // Assert
            var items = Assert.IsType<ParkingSpotDTO>(getResult.Value);
            Assert.Equal(0.30m, items.priceHour);
            Assert.IsType<NoContentResult>(response.Result);
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

    public static class PublicPark_ParkingSpotsContext
    {
        private static PublicParkContext publicParkingSpotsContext;
        public static PublicParkContext GetPublicParkContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<PublicParkContext>()
                            .UseInMemoryDatabase(databaseName: dbName)
                            .Options;

            publicParkingSpotsContext = new PublicParkContext(options);
            Seed();
            return publicParkingSpotsContext;
        }

        private static void Seed()
        {
            publicParkingSpotsContext.ParkingSpots.Add(new ParkingSpot { parkingSpotID = "A1", priceHour = 0.250m });
            publicParkingSpotsContext.ParkingSpots.Add(new ParkingSpot { parkingSpotID = "E1", priceHour = 0.5m });
            publicParkingSpotsContext.ParkingSpots.Add(new ParkingSpot { parkingSpotID = "I1", priceHour = 0.9m });
            publicParkingSpotsContext.ParkingSpots.Add(new ParkingSpot { parkingSpotID = "O1", priceHour = 1.00m });
            publicParkingSpotsContext.ParkingSpots.Add(new ParkingSpot { parkingSpotID = "A3", priceHour = 0.25m });
            publicParkingSpotsContext.SaveChanges();
        }
    }
}