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
    public class ReservationsControllerTest
    {

        [Fact]
        public async Task GetAllReservationsAsync_ShouldReturnAllReservationsAsync()
        {
            // Arrange 
            var TestContext = PrivatePark_ReservationsContext.GetPrivateParkContext("getAllReservations");
            var reservationRepository = new ReservationRepository(TestContext);
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, reservationRepository, mapper);
            var reservationService = new ReservationService(reservationRepository, parkingSpotRepository, mapper);
            var theController = new ReservationsController(reservationService, parkingSpotService);

            //Act
            var result = await theController.GetReservations();

            //Assert
            var reservations = Assert.IsType<List<ReservationDTO>>(result.Value);
            Assert.Equal(8, reservations.Count);
        }

        [Fact]
        public async Task GetReservationByID_ShouldReturnNotFound()
        {
            var TestContext = PrivatePark_ReservationsContext.GetPrivateParkContext("getReservationnotFound");
            var reservationRepository = new ReservationRepository(TestContext);
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, reservationRepository, mapper);
            var reservationService = new ReservationService(reservationRepository, parkingSpotRepository, mapper);
            var theController = new ReservationsController(reservationService, parkingSpotService);

            var result = await theController.GetReservation("1");

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetReservationByID_ShouldReturnReservationByID()
        {
            //Arrange
            var TestContext = PrivatePark_ReservationsContext.GetPrivateParkContext("getReservationbyID");
            var reservationRepository = new ReservationRepository(TestContext);
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, reservationRepository, mapper);
            var reservationService = new ReservationService(reservationRepository, parkingSpotRepository, mapper);
            var theController = new ReservationsController(reservationService, parkingSpotService);

            //Act
            var result = await theController.GetReservation("ABC2");


            //Assert
            var items = Assert.IsType<ReservationDTO>(result.Value);
            Assert.Equal(DateTime.Parse("2021-02-01 07:00:00"), items.startTime);
        }


        [Fact]
        public async Task PostReservation_ShouldCreateNewReservation()
        {
            var TestContext = PrivatePark_ReservationsContext.GetPrivateParkContext("postReservation");
            var reservationRepository = new ReservationRepository(TestContext);
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, reservationRepository, mapper);
            var reservationService = new ReservationService(reservationRepository, parkingSpotRepository, mapper);
            var theController = new ReservationsController(reservationService, parkingSpotService);

            var newReservation = new ReservationDTO
            {
                reservationID = "1",
                startTime = DateTime.Parse("2021-05-21 08:00:00"),
                hours = 3,
                parkingSpotID = "E1"
            };
            var result = await theController.PostReservation(newReservation);
            var getResult = await theController.GetReservation("1");


            var items = Assert.IsType<ReservationDTO>(getResult.Value);
            Assert.Equal(3, items.hours);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }




        [Fact]
        public async Task PostNoStartTImeIDReservationAsync_ShouldReturnBadRequest()
        {
            // Arrange
            var TestContext = PrivatePark_ReservationsContext.GetPrivateParkContext("postReservationNoDate");
            var reservationRepository = new ReservationRepository(TestContext);
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, reservationRepository, mapper);
            var reservationService = new ReservationService(reservationRepository, parkingSpotRepository, mapper);
            var theController = new ReservationsController(reservationService, parkingSpotService);
            var noParkingLotID = new ReservationDTO
            {
                reservationID = "1",
                hours = 4,
                parkingSpotID = "E2"
            };
            theController.ModelState.AddModelError("startTime", "Required");

            // Act
            var response = await theController.PostReservation(noParkingLotID);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        //        [Fact]
        //        public async Task DeleteReservation_ShouldDeleteReservation()
        //        {
        //            Thread.Sleep(200);
        //            // Arrange
        //            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //            var TestContext = TodoContextMocker.GetPrivateParkContext(dbName);
        //            var theController = new ReservationsController(TestContext);

        //            // Act
        //            var result = await theController.DeleteReservation("1");

        //            //Assert 
        //            Assert.IsType<NotFoundObjectResult>(result);
        //        }

        //        [Fact]
        //        public async Task DeleteNotExistReservation_ShouldReturnNotFound()
        //        {
        //            Thread.Sleep(3000);
        //            // Arrange
        //            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //            var TestContext = TodoContextMocker.GetPrivateParkContext(dbName);
        //            var theController = new ReservationsController(TestContext);
        //            var testCod = "123";

        //            // Act
        //            var result = await theController.DeleteReservation(testCod);

        //            // Assert
        //            Assert.IsType<NotFoundObjectResult>(result);
        //        }



        //        [Fact]
        //        public async Task PutNoExistingReservationAsync_ShouldReturnNotFound()
        //        {
        //            Thread.Sleep(3000);
        //            // Arrange
        //            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //            var testContext = TodoContextMocker.GetPrivateParkContext(dbName);
        //            var theController = new ReservationsController(testContext);
        //            var testCod = "AAA";

        //            var theNonReservation = new Reservation
        //            {
        //                reservationID = testCod,
        //                startTime = DateTime.Parse("2022-05-22 07:00:00"),
        //                hours = 2,
        //                parkingSpotID = "E1"
        //            };

        //            // Act
        //            var response = await theController.PutReservation(testCod, theNonReservation);

        //            // Assert
        //            Assert.IsType<NotFoundObjectResult>(response);
        //        }

        //        [Fact]
        //        public async Task PutNoParkingSpotID_ShouldReturnBadRequestResult()
        //        {
        //            Thread.Sleep(2000);
        //            // Arrange
        //            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //            var testContext = TodoContextMocker.GetPrivateParkContext(dbName);
        //            var theController = new ReservationsController(testContext);
        //            var testCod = "ABC8";

        //            var noParkingSpotID = new Reservation
        //            {
        //                reservationID = testCod,
        //                startTime = DateTime.Parse("2021-03-22 09:00:00"),
        //                hours = 2,
        //                endTime = DateTime.Parse("2021-03-27 09:00:00")
        //            };

        //            var c = await testContext.FindAsync<Reservation>(testCod);
        //            testContext.Entry(c).State = EntityState.Detached;

        //            theController.ModelState.AddModelError("parkingSpotID", "Required");

        //            // Act
        //            var response = await theController.PutReservation(testCod, noParkingSpotID);

        //            // Assert
        //            Assert.IsType<BadRequestObjectResult>(response);
        //        }

        //        [Fact]
        //        public async Task PutNoStartTimeReservation_ShouldReturnBadRequest()
        //        {
        //            Thread.Sleep(2000);
        //            // Arrange
        //            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //            var testContext = TodoContextMocker.GetPrivateParkContext(dbName);
        //            var theController = new ReservationsController(testContext);
        //            var testCod = "ABC8";

        //            var noStartTimeReservation = new Reservation
        //            {
        //                reservationID = testCod,
        //                hours = 2,
        //                endTime = DateTime.Parse("2021-03-21 19:00:00"),
        //                parkingSpotID = "A1"
        //            };

        //            var c = await testContext.FindAsync<Reservation>(testCod);
        //            testContext.Entry(c).State = EntityState.Detached;

        //            theController.ModelState.AddModelError("startTime", "Required");

        //            // Act
        //            var response = await theController.PutReservation(testCod, noStartTimeReservation);

        //            // Assert
        //            Assert.IsType<BadRequestObjectResult>(response);
        //        }

        //        [Fact]
        //        public async Task PutNoEndTimeReservation_ShouldReturnBadRequest()
        //        {
        //            Thread.Sleep(2000);
        //            // Arrange
        //            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //            var testContext = TodoContextMocker.GetPrivateParkContext(dbName);
        //            var theController = new ReservationsController(testContext);
        //            var testCod = "ABC8";

        //            var noEndTimeReservation = new Reservation
        //            {
        //                reservationID = testCod,
        //                startTime = DateTime.Parse("2021-03-22 13:00:00"),
        //                hours = 2
        //            };

        //            var c = await testContext.FindAsync<Reservation>(testCod);
        //            testContext.Entry(c).State = EntityState.Detached;

        //            theController.ModelState.AddModelError("endTime", "Required");

        //            // Act
        //            var response = await theController.PutReservation(testCod, noEndTimeReservation);

        //            // Assert
        //            Assert.IsType<BadRequestObjectResult>(response);
        //        }

        //        [Fact]
        //        public async Task PutReservation_ShouldReturnCreatedResponse()
        //        {
        //            Thread.Sleep(3500);
        //            // Arrange
        //            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //            var testContext = TodoContextMocker.GetPrivateParkContext(dbName);
        //            var theController = new ReservationsController(testContext);
        //            var testCod = "ABC8";
        //            var theReservation = new Reservation
        //            {
        //                reservationID = testCod,
        //                startTime = DateTime.Parse("2021-03-21 09:00:00"),
        //                hours = 3,
        //                parkingSpotID = "E1"
        //            };

        //            var c = await testContext.FindAsync<Reservation>(testCod);
        //            testContext.Entry(c).State = EntityState.Detached;

        //            // Act
        //            var response = await theController.PutReservation(testCod, theReservation);
        //            var getResult = await theController.GetReservation(theReservation.reservationID);

        //            // Assert
        //            var items = Assert.IsType<Reservation>(getResult.Value);
        //            Assert.Equal("E1", items.parkingSpotID);
        //            Assert.IsType<NoContentResult>(response);
        //        }
    }
    public static class PrivatePark_ReservationsContext
    {
        private static PrivateParkContext reservationsContext;

        public static PrivateParkContext GetPrivateParkContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<PrivateParkContext>()
                            .UseInMemoryDatabase(databaseName: dbName)
                            .Options;

            reservationsContext = new PrivateParkContext(options);
            Seed();
            return reservationsContext;
        }
        private static void Seed()
        {
            reservationsContext.Reservations.Add(new Reservation { reservationID = "ABC1", isCancelled = false, startTime = DateTime.Parse("2021-01-28 07:00:00"), hours = 1, endTime = DateTime.Parse("2021-05-22 08:00:00"), parkingSpotID = "A1", });
            reservationsContext.Reservations.Add(new Reservation { reservationID = "ABC2", isCancelled = true, startTime = DateTime.Parse("2021-02-01 07:00:00"), hours = 2, endTime = DateTime.Parse("2021-09-10 09:00:00"), parkingSpotID = "E1", });
            reservationsContext.Reservations.Add(new Reservation { reservationID = "ABC3", isCancelled = false, startTime = DateTime.Parse("2021-09-22 07:00:00"), hours = 12, endTime = DateTime.Parse("2021-09-22 19:00:00"), parkingSpotID = "I1" });
            reservationsContext.Reservations.Add(new Reservation { reservationID = "ABC4", isCancelled = false, startTime = DateTime.Parse("2021-10-22 07:00:00"), hours = 3, endTime = DateTime.Parse("2021-10-22 10:00:00"), parkingSpotID = "O1", });
            reservationsContext.Reservations.Add(new Reservation { reservationID = "ABC5", isCancelled = false, startTime = DateTime.Parse("2021-09-22 07:00:00"), hours = 1, endTime = DateTime.Parse("2021-09-22 08:00:00"), parkingSpotID = "A3", });
            reservationsContext.Reservations.Add(new Reservation { reservationID = "ABC6", isCancelled = false, startTime = DateTime.Parse("2021-08-22 12:00:00"), hours = 1, endTime = DateTime.Parse("2021-08-22 13:00:00"), parkingSpotID = "A1", });
            reservationsContext.Reservations.Add(new Reservation { reservationID = "ABC7", isCancelled = true, startTime = DateTime.Parse("2021-07-22 14:00:00"), hours = 1, endTime = DateTime.Parse("2021-07-22 15:00:00"), parkingSpotID = "A1", });
            reservationsContext.Reservations.Add(new Reservation { reservationID = "ABC8", isCancelled = false, startTime = DateTime.Parse("2021-06-22 18:00:00"), hours = 1, endTime = DateTime.Parse("2021-06-22 19:00:00"), parkingSpotID = "I1", });
            reservationsContext.SaveChanges();
        }
    }
}
