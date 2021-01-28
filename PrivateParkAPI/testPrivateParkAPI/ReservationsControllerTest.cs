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

        //[Fact]
        //public async Task PostReservation_ShouldCreateNewReservation()
        //{
        //    Thread.Sleep(800);
        //    var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //    var TestContext = TodoContextMocker.GetPrivateParkContext(dbName);
        //    var theController = new ReservationsController(TestContext);

        //    var newReservation = new Reservation
        //    {
        //        reservationID = "1",
        //        startTime = DateTime.Parse("2021-05-21 08:00:00"),
        //        hours = 3,
        //        endTime = DateTime.Parse("2021-05-23 19:00:00"),
        //        parkingSpotID = "A1"
        //    };
        //    var result = await theController.PostReservation(newReservation);
        //    var getResult = await theController.GetReservation("1");

        //    var items = Assert.IsType<Reservation>(getResult.Value);
        //    Assert.Equal(2, items.hours);
        //    Assert.IsType<CreatedAtActionResult>(result.Result);
        //}

        //[Fact]
        //public async Task GetReservationByID_ShouldReturnReservationByID()
        //{
        //    //Arrange
        //    Thread.Sleep(2000);
        //    var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //    var TestContext = TodoContextMocker.GetPrivateParkContext(dbName);
        //    var theController = new ReservationsController(TestContext);

        //    //Act
        //    var result = await theController.GetReservation("123ABC");

        //    //Assert
        //    var items = Assert.IsType<Reservation>(result.Value);
        //    Assert.Equal(DateTime.Parse("2021-05-22 07:00:00"), items.startTime);
        //}

        //[Fact]
        //public async Task GetReservationAsync_ShouldReturnReservation()
        //{
        //    Thread.Sleep(1000);
        //    // Arrange
        //    var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //    var testContext = TodoContextMocker.GetPrivateParkContext(dbName);
        //    var theController = new ReservationsController(testContext);
        //    var testCod = "123ABC";

        //    // Act
        //    var response = await theController.GetReservation(testCod);

        //    //Assert     
        //    Assert.IsType<Reservation>(response.Value);
        //}

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

        //[Fact]
        //public async Task GetReservationAsync_ShouldReturnTheRightItemAsync()
        //{
        //    Thread.Sleep(800);
        //    // Arrange
        //    var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //    var testContext = TodoContextMocker.GetPrivateParkContext(dbName);
        //    var theController = new ReservationsController(testContext);
        //    var testCod = "C1";

        //    // Act
        //    var result = await theController.GetReservation(testCod);
        //    var reservationItem = result.Value;

        //    //Assert     
        //    Assert.IsType<Reservation>(reservationItem);
        //    Assert.Equal(testCod, reservationItem.reservationID);
        //}

        //[Fact]
        //public async Task DeleteReservation_ShouldDeleteReservation()
        //{

        //    Thread.Sleep(3000);
        //    // Arrange
        //    var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //    var TestContext = TodoContextMocker.GetPrivateParkContext(dbName);
        //    var theController = new ReservationsController(TestContext);
        //    var testCod= "123ABC";

        //    // Act
        //    var result = await theController.DeleteReservation(testCod);
        //    var response = await theController.GetReservation(testCod);

        //    //Assert 
        //    Assert.IsType<NotFoundResult>(response.Result);
        //}

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
    }
}
