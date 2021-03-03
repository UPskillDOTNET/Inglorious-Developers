using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrivateParkAPI.Controllers;
using PrivateParkAPI.Data;
using PrivateParkAPI.DTO;
using PrivateParkAPI.Models;
using PrivateParkAPI.Repositories.Repository;
using PrivateParkAPI.Services.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace testPrivateParkAPI
{
    public class ParkingSpotsControllerTest
    {
        [Fact]
        public async Task GetAllnotPrivateAsync_ShouldReturnAllnotPrivateAsync()
        {
            // Arrange 
            var TestContext = PrivatePark_ParkingSpotsContext.GetPrivateParkContext("GetAllnotPrivateParkingSpots");
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var reservationRepository = new ReservationRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, reservationRepository, mapper);
            var theController = new ParkingSpotsController(parkingSpotService);
            // act
            var result = await theController.GetAllNotPrivate();
            //assert
            var items = Assert.IsType<List<ParkingSpotDTO>>(result.Value);
            Assert.Equal(3, items.Count);
        }
        [Fact]
        public async Task GetAllAvailableAsync_ShouldReturnAllAvailableAsync()
        {

            // Arrange 
            var TestContext = PrivatePark_ParkingSpotsContext.GetPrivateParkContext("GetAllAvailableParkingSpots");
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var reservationRepository = new ReservationRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, reservationRepository, mapper);
            var theController = new ParkingSpotsController(parkingSpotService);
            // Act
            var result = await theController.GetFreeParkingSpots();
            //Assert
            var items = Assert.IsType<List<ParkingSpotDTO>>(result.Value);
            Assert.Equal(2, items.Count);
        }
        [Fact]
        public async Task GetAllAvailableinHourAsync_ShouldReturnAllAvailableInHourAsync()
        {

            // Arrange 
            var TestContext = PrivatePark_ParkingSpotsContext.GetPrivateParkContext("GetAllinHourParkingSpots");
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var reservationRepository = new ReservationRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, reservationRepository, mapper);
            var theController = new ParkingSpotsController(parkingSpotService);
            // Act
            var result = await theController.GetFreeParkingSpotsByDate(DateTime.Parse("2021-08-22 07:00:00"), DateTime.Parse("2021-09-22 19:00:00"));
            //Assert
            var items = Assert.IsType<List<ParkingSpotDTO>>(result.Value);
            Assert.Equal(2, items.Count);
        }
        [Fact]
        public async Task GetAllAsync_ShouldReturnAllAsync()
        {
            // Arrange 
            var TestContext = PrivatePark_ParkingSpotsContext.GetPrivateParkContext("GetAllParkingSpots");
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var reservationRepository = new ReservationRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, reservationRepository, mapper);
            var theController = new ParkingSpotsController(parkingSpotService);
            // Act
            var result = await theController.GetAllParkingSpots();
            //Assert
            var items = Assert.IsType<List<ParkingSpotDTO>>(result.Value);
            Assert.Equal(5, items.Count);
        }

        [Fact]
        public async Task GetParkingSpotAsync_ShouldReturnNotFound()
        {

            // Arrange 
            var TestContext = PrivatePark_ParkingSpotsContext.GetPrivateParkContext("notGetParkingSpot");
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var reservationRepository = new ReservationRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, reservationRepository, mapper);
            var theController = new ParkingSpotsController(parkingSpotService);
            var testCod = "S150";
            // Act
            var result = await theController.GetParkingSpot(testCod);
            //Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetParkingSpotAsync_ShouldReturnParkingSpot()
        {

            // Arrange 
            var TestContext = PrivatePark_ParkingSpotsContext.GetPrivateParkContext("GetParkingSpot");
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var reservationRepository = new ReservationRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, reservationRepository, mapper);
            var theController = new ParkingSpotsController(parkingSpotService);
            var testCod = "E1";
            // Act
            var result = await theController.GetParkingSpot(testCod);
            //Assert
            Assert.IsType<ParkingSpotDTO>(result.Value);
        }


        [Fact]
        public async Task GetTheRightParkingSpotAsync_ShouldReturnTheRightParkingSpot()
        {

            // Arrange 
            var TestContext = PrivatePark_ParkingSpotsContext.GetPrivateParkContext("GetrightParkingSpot");
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var reservationRepository = new ReservationRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, reservationRepository, mapper);
            var theController = new ParkingSpotsController(parkingSpotService);
            var testCod = "E1";

            // Act
            var result = await theController.GetParkingSpot(testCod);
            var parkingSpotItem = result.Value;

            //Assert
            Assert.IsType<ParkingSpotDTO>(result.Value);
            Assert.Equal(testCod, parkingSpotItem.parkingSpotID);
        }


        [Fact]
        public async Task postnoPriceasync_shouldreturnbadrequest()
        {

            // Arrange 
            var TestContext = PrivatePark_ParkingSpotsContext.GetPrivateParkContext("postbadParkingSpot");
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var reservationRepository = new ReservationRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, reservationRepository, mapper);
            var theController = new ParkingSpotsController(parkingSpotService);
            var nopricespot = new ParkingSpotDTO
            {
                parkingSpotID = "A5",
                floor = 1,
                isCovered = false,
                isPrivate = true,
            };

            // Act
            var result = await theController.PostParkingSpot(nopricespot);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task postnoParkingLotIDasync_shouldreturnbadrequest()
        {

            // Arrange 
            var TestContext = PrivatePark_ParkingSpotsContext.GetPrivateParkContext("postbadParkingSpot2");
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var reservationRepository = new ReservationRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, reservationRepository, mapper);
            var theController = new ParkingSpotsController(parkingSpotService);
            var noLotIDspot = new ParkingSpotDTO
            {
                parkingSpotID = "A5",
                priceHour = 1.30M,
                floor = 1,
                isCovered = false,
                isPrivate = true,

            };
            // Act
            var result = await theController.PostParkingSpot(noLotIDspot);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task PostValidParkingSlotasync_ShouldReturnCreatedResponse()
        {

            // Arrange 
            var TestContext = PrivatePark_ParkingSpotsContext.GetPrivateParkContext("postvalidParkingSpot");
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var reservationRepository = new ReservationRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, reservationRepository, mapper);
            var theController = new ParkingSpotsController(parkingSpotService);
            var parking = new ParkingSpotDTO
            {
                parkingSpotID = "A5",
                priceHour = 1.30M,
                floor = 1,
                isCovered = false,
                isPrivate = true,

            };
            // Act
            var result = await theController.PostParkingSpot(parking);


            //Assert
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }


        [Fact]
        public async Task PostParkingSlotAsync_ShouldCreateAParkingSlotAsync()
        {
            // Arrange
            var TestContext = PrivatePark_ParkingSpotsContext.GetPrivateParkContext("postParkingSpotisParkingSpot");
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var reservationRepository = new ReservationRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, reservationRepository, mapper);
            var theController = new ParkingSpotsController(parkingSpotService);
            var parkingID = "A6";
            var parking = new ParkingSpotDTO
            {
                parkingSpotID = "A6",
                priceHour = 1.50M,
                floor = 0,
                isCovered = false,
                isPrivate = false,

            };

            // Act

            var result = await theController.PostParkingSpot(parking);
            var response = await theController.GetParkingSpot(parkingID);

            // Assert
            Assert.NotNull(response);
            Assert.IsNotType<BadRequestObjectResult>(response);
            Assert.IsType<ParkingSpotDTO>(response.Value);

            Assert.Equal(0, response.Value.floor);
        }


        //        [Fact]
        //        public async Task PutNoExistingParkingSpotAsync_ShouldReturnNotFound()
        //        {
        //            // Arrange
        //            var TestContext = TodoContextMocker.GetPrivateParkContext("postParkingSpotnotFound");
        //            var theController = new ParkingSpotsController(TestContext);

        //            var parking = new ParkingSpot
        //            {
        //                parkingSpotID = "A36",
        //                priceHour = 1.50M,
        //                floor = 0,
        //                isCovered = true,
        //                isPrivate = false,
        //                parkingLotID = 2

        //            };

        //            // Act
        //            var response = await theController.PutParkingSpot("A36", parking);

        //            // Assert
        //            Assert.IsType<NotFoundObjectResult>(response);
        //        }



        //        [Fact]
        //        public async Task PutNofloorAsync_ShouldReturnBadRequest()
        //        {
        //            // Arrange
        //            var TestContext = TodoContextMocker.GetPrivateParkContext("putParkingBadRequest");
        //            var theController = new ParkingSpotsController(TestContext);

        //            var parkingID = "E1";
        //            var parking = new ParkingSpot
        //            {
        //               parkingSpotID = "E1",
        //               priceHour = 1.50M,
        //               isCovered = true,
        //               isPrivate = true,
        //               parkingLotID =1

        //            };

        //            var c = await TestContext.FindAsync<ParkingSpot>(parkingID); //To Avoid tracking error
        //            TestContext.Entry(c).State = EntityState.Detached;

        //            theController.ModelState.AddModelError("floor", "Required");

        //            // Act
        //            var response = await theController.PutParkingSpot(parkingID, parking);

        //            // Assert
        //            Assert.IsType<BadRequestResult>(response);
        //        }

        //        [Fact]
        //        public async Task PutNoPriceAsync_ShouldReturnBadRequest()
        //        {
        //            // Arrange
        //            var TestContext = TodoContextMocker.GetPrivateParkContext("putParkingBadRequest2");
        //            var theController = new ParkingSpotsController(TestContext);

        //            var parkingID = "A1";
        //            var parking = new ParkingSpot
        //            {
        //                parkingSpotID = "A1",
        //                floor = 1,
        //                isCovered = false,
        //                isPrivate = true,
        //                parkingLotID = 1

        //            };

        //            var c = await TestContext.FindAsync<ParkingSpot>(parkingID); //To Avoid tracking error
        //            TestContext.Entry(c).State = EntityState.Detached;

        //            theController.ModelState.AddModelError("priceHour", "Required");

        //            // Act
        //            var response = await theController.PutParkingSpot(parkingID, parking);

        //            // Assert
        //            Assert.IsType<BadRequestResult>(response);
        //        }


        [Fact]
        public async Task PutParkingSpot_ShouldReturnCreatedResponse()
        {
            // Arrange
            var TestContext = PrivatePark_ParkingSpotsContext.GetPrivateParkContext("putParkingisParking");
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var reservationRepository = new ReservationRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, reservationRepository, mapper);
            var theController = new ParkingSpotsController(parkingSpotService);
            var parkingID = "E1";
            var parking = new ParkingSpotDTO
            {
                parkingSpotID = "E1",
                priceHour = 1.75M,
                floor = 1,
                isCovered = true,
                isPrivate = true,

            };

            var c = await TestContext.FindAsync<ParkingSpot>(parkingID); //To Avoid tracking error
            TestContext.Entry(c).State = EntityState.Detached;

            // Act
            var response = await theController.PutParkingSpot(parkingID, parking);

            // Assert
            Assert.IsType<NoContentResult>(response.Result);
        }

        [Fact]
        public async Task DeleteNotExistingParkingSpot_ShouldReturnNotFound()
        {
            // Arrange
            var TestContext = PrivatePark_ParkingSpotsContext.GetPrivateParkContext("deleteParkingNotFound");
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var reservationRepository = new ReservationRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, reservationRepository, mapper);
            var theController = new ParkingSpotsController(parkingSpotService);
            var parkingID = "E7";

            // Act
            var result = await theController.DeleteParkingSpot(parkingID);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task DeleteExistingParkingSpotAsync_ShouldReturnOkResult()
        {
            // Arrange
            var TestContext = PrivatePark_ParkingSpotsContext.GetPrivateParkContext("deleteParkingOK");
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var reservationRepository = new ReservationRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, reservationRepository, mapper);
            var theController = new ParkingSpotsController(parkingSpotService);
            var parkingID = "E1";

            // Act
            var response = await theController.DeleteParkingSpot(parkingID);

            // Assert
            Assert.IsType<NoContentResult>(response.Result);
        }

        [Fact]
        public async Task DeleteExistingParkingSpotAsync_ShouldRemovetheParkingSpotAsync()
        {
            // Arrange
            var TestContext = PrivatePark_ParkingSpotsContext.GetPrivateParkContext("deletedParkIsDeleted");
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var reservationRepository = new ReservationRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, reservationRepository, mapper);
            var theController = new ParkingSpotsController(parkingSpotService);
            var parkingID = "A1";

            // Act
            var response = await theController.DeleteParkingSpot(parkingID);

            var notExists = await theController.GetParkingSpot(parkingID);

            // Assert
            Assert.IsType<NotFoundObjectResult>(notExists.Result);
        }
    }
    public static class PrivatePark_ParkingSpotsContext
    {
        private static PrivateParkContext parkingSpotsContext;

        public static PrivateParkContext GetPrivateParkContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<PrivateParkContext>()
                            .UseInMemoryDatabase(databaseName: dbName)
                            .Options;

            parkingSpotsContext = new PrivateParkContext(options);
            Seed();
            return parkingSpotsContext;
        }

        private static void Seed()
        {
            parkingSpotsContext.ParkingSpots.Add(new ParkingSpot { parkingSpotID = "A1", priceHour = 0.250m, isCovered = false, isPrivate = true, floor = 1});
            parkingSpotsContext.ParkingSpots.Add(new ParkingSpot { parkingSpotID = "E1", priceHour = 0.5m, isCovered = true, isPrivate = false, floor = 2});
            parkingSpotsContext.ParkingSpots.Add(new ParkingSpot { parkingSpotID = "I1", priceHour = 0.9m, isCovered = false, isPrivate = true, floor = 1});
            parkingSpotsContext.ParkingSpots.Add(new ParkingSpot { parkingSpotID = "O1", priceHour = 1.00m, isCovered = true, isPrivate = false});
            parkingSpotsContext.ParkingSpots.Add(new ParkingSpot { parkingSpotID = "A3", priceHour = 0.25m, isCovered = false, isPrivate = false});

            parkingSpotsContext.SaveChanges();
        }
    }
}

