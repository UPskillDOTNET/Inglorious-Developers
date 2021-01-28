using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using PrivateParkAPI.Models;
using testProject;
using PrivateParkAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace testPrivateParkAPI
{
    public class ReservationsControllerTest
    {

        [Fact]
        public async Task GetAllReservationsAsync_ShouldReturnAllReservationsAsync()
        {
            Thread.Sleep(300);
            // Arrange 
            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            var testContext = TodoContextMocker.GetPrivateParkContext(dbName);
            var theController = new ReservationsController(testContext);
            //Act
            var result = await theController.GetReservations();

            //Assert
            var reservations = Assert.IsType<List<Reservation>>(result.Value);
            Assert.Equal(8, reservations.Count);
        }

        [Fact]
        public async Task GetReservationByID_ShouldReturnNotFound()
        {
            Thread.Sleep(4000);
            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            var TestContext = TodoContextMocker.GetPrivateParkContext(dbName);
            var theController = new ReservationsController(TestContext);

            var result = await theController.GetReservation("1");

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetReservationAsync_ShouldReturnNotFound()
        {
            Thread.Sleep(300);
            // Arrange
            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            var testContext = TodoContextMocker.GetPrivateParkContext(dbName);
            var theController = new ReservationsController(testContext);

            // Act
            var response = await theController.GetReservation("1");

            //Assert
            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public async Task PostReservation_ShouldCreateNewReservation()
        {
            Thread.Sleep(1500);
            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            var TestContext = TodoContextMocker.GetPrivateParkContext(dbName);
            var theController = new ReservationsController(TestContext);

            var newReservation = new Reservation
            {
                reservationID = "1",
                startTime = DateTime.Parse("2021-05-21 08:00:00"),
                hours = 3,
                endTime = DateTime.Parse("2021-05-23 19:00:00"),
                parkingSpotID = "A1"
            };
            var result = await theController.PostReservation(newReservation);
            var getResult = await theController.GetReservation("1");


            var items = Assert.IsType<Reservation>(getResult.Value);
            Assert.Equal(3, items.hours);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public async Task GetReservationByID_ShouldReturnReservationByID()
        {
            //Arrange
            Thread.Sleep(2000);
            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            var TestContext = TodoContextMocker.GetPrivateParkContext(dbName);
            var theController = new ReservationsController(TestContext);

            //Act
            var result = await theController.GetReservation("ABC1");


            //Assert
            var items = Assert.IsType<Reservation>(result.Value);
            Assert.Equal(DateTime.Parse("2021-05-22 07:00:00"), items.startTime);
        }


        [Fact]
        public async Task PostNoStartTImeIDReservationAsync_ShouldReturnBadRequest()
        {
            Thread.Sleep(2500);
            // Arrange
            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            var testContext = TodoContextMocker.GetPrivateParkContext(dbName);
            var theController = new ReservationsController(testContext);
            var noParkingLotID = new Reservation
            {
                reservationID = "1",
                hours = 4,
                endTime = DateTime.Parse("2021-03-12 19:00:00"),
                parkingSpotID = "E2"
            };
            theController.ModelState.AddModelError("startTime", "Required");

            // Act
            var response = await theController.PostReservation(noParkingLotID);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public async Task DeleteReservation_ShouldDeleteReservation()
        {
            Thread.Sleep(200);
            // Arrange
            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            var TestContext = TodoContextMocker.GetPrivateParkContext(dbName);
            var theController = new ReservationsController(TestContext);

            // Act
            var result = await theController.DeleteReservation("1");

            //Assert 
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteNotExistReservation_ShouldReturnNotFound()
        {
            Thread.Sleep(3000);
            // Arrange
            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            var TestContext = TodoContextMocker.GetPrivateParkContext(dbName);
            var theController = new ReservationsController(TestContext);
            var testCod = "123";

            // Act
            var result = await theController.DeleteReservation(testCod);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task PostWithoutEndTimeReservationAsync_ShouldReturnBadRequest()
        {
            Thread.Sleep(4000);
            // Arrange
            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            var testContext = TodoContextMocker.GetPrivateParkContext(dbName);
            var theController = new ReservationsController(testContext);
            var nParkingLotID = new Reservation
            {
                reservationID = "2",
                startTime = DateTime.Parse("2021-02-18 19:00:00"),
                hours = 4,
                parkingSpotID = "3D"
            };

            theController.ModelState.AddModelError("endTime", "Required");

            // Act
            var response = await theController.PostReservation(nParkingLotID);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public async Task PutNoExistingReservationAsync_ShouldReturnNotFound()
        {
            Thread.Sleep(3000);
            // Arrange
            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            var testContext = TodoContextMocker.GetPrivateParkContext(dbName);
            var theController = new ReservationsController(testContext);
            var testCod = "AAA";

            var theNonReservation = new Reservation
            {
                reservationID = testCod,
                startTime = DateTime.Parse("2022-05-22 07:00:00"),
                hours = 2,
                endTime = DateTime.Parse("2022-05-22 09:00:00"),
                parkingSpotID = "Z1"
            };

            // Act
            var response = await theController.PutReservation(testCod, theNonReservation);

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async Task PutNoParkingSpotID_ShouldReturnBadRequestResult()
        {
            Thread.Sleep(3500);
            // Arrange
            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            var testContext = TodoContextMocker.GetPrivateParkContext(dbName);
            var theController = new ReservationsController(testContext);
            var testCod = "ABC8";

            var noParkingSpotID = new Reservation
            {
                reservationID = testCod,
                startTime = DateTime.Parse("2021-03-22 09:00:00"),
                hours = 2,
                endTime = DateTime.Parse("2021-03-27 09:00:00")
            };

            var c = await testContext.FindAsync<Reservation>(testCod);
            testContext.Entry(c).State = EntityState.Detached;

            theController.ModelState.AddModelError("parkingSpotID", "Required");

            // Act
            var response = await theController.PutReservation(testCod, noParkingSpotID);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task PutNoStartTimeReservation_ShouldReturnBadRequest()
        {
            Thread.Sleep(3500);
            // Arrange
            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            var testContext = TodoContextMocker.GetPrivateParkContext(dbName);
            var theController = new ReservationsController(testContext);
            var testCod = "ABC1";

            var noStartTimeReservation = new Reservation
            {
                reservationID = testCod,
                hours = 2,
                endTime = DateTime.Parse("2021-03-21 19:00:00"),
                parkingSpotID = "A1"
            };

            var c = await testContext.FindAsync<Reservation>(testCod);
            testContext.Entry(c).State = EntityState.Detached;

            theController.ModelState.AddModelError("startTime", "Required");

            // Act
            var response = await theController.PutReservation(testCod, noStartTimeReservation);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task PutNoEndTimeReservation_ShouldReturnBadRequest()
        {
            Thread.Sleep(3500);
            // Arrange
            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            var testContext = TodoContextMocker.GetPrivateParkContext(dbName);
            var theController = new ReservationsController(testContext);
            var testCod = "ABC1";

            var noEndTimeReservation = new Reservation
            {
                reservationID = testCod,
                startTime = DateTime.Parse("2021-03-22 13:00:00"),
                hours = 2
            };

            var c = await testContext.FindAsync<Reservation>(testCod);
            testContext.Entry(c).State = EntityState.Detached;

            theController.ModelState.AddModelError("endTime", "Required");

            // Act
            var response = await theController.PutReservation(testCod, noEndTimeReservation);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task PutReservation_ShouldReturnCreatedResponse()
        {
            Thread.Sleep(3500);
            // Arrange
            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            var testContext = TodoContextMocker.GetPrivateParkContext(dbName);
            var theController = new ReservationsController(testContext);
            var testCod = "ABC1";
            var theReservation = new Reservation
            {
                reservationID = testCod,
                startTime = DateTime.Parse("2021-03-21 09:00:00"),
                hours = 3,
                endTime = DateTime.Parse("2021-04-21 19:00:00"),
                parkingSpotID = "E4"
            };

            var c = await testContext.FindAsync<Reservation>(testCod);
            testContext.Entry(c).State = EntityState.Detached;

            // Act
            var response = await theController.PutReservation(testCod, theReservation);
            var getResult = await theController.GetReservation(theReservation.reservationID);

            // Assert
            var items = Assert.IsType<Reservation>(getResult.Value);
            Assert.Equal("E4", items.parkingSpotID);
            Assert.IsType<NoContentResult>(response);
        }
    }
}
