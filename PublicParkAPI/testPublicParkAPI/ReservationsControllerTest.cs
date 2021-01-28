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
using System.Threading;
using Microsoft.AspNetCore.Mvc;

namespace testPublicParkAPI {
    public class ReservationsControllerTest {

        [Fact]
        public async Task GetAllReservationsAsync_ShouldReturnAllReservationsAsync() {
            // Arrange 
            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            //Arrange
            var TestContext = TodoContextMocker.GetPublicParkContext(dbName);
            var theController = new ReservationsController(TestContext);
            //Act
            var result = await theController.GetReservations();

            //Assert
            var items = Assert.IsType<List<Reservation>>(result.Value);
            Assert.Equal(8, items.Count);
        }

        [Fact]
        public async Task GetRecomendationByID_ShouldReturnNotFound() {
            Thread.Sleep(4000);
            var TestContext = TodoContextMocker.GetPublicParkContext("NotFoundRecomendationByID");
            var theController = new ReservationsController(TestContext);

            var result = await theController.GetReservation("1");

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostReservation_ShouldCreateNewReservation() {
            Thread.Sleep(4000);

            var TestContext = TodoContextMocker.GetPublicParkContext("PostReservation");
            var theController = new ReservationsController(TestContext);

            var newReservation = new Reservation {
                reservationID = "1",
                startTime = DateTime.Parse("2022-05-22 07:00:00"),
                hours = 2,
                endTime = DateTime.Parse("2022-05-22 09:00:00"),
                parkingSpotID = "A1"
            };
            var result = await theController.PostReservation(newReservation);
            var getResult = await theController.GetReservation("1");


            var items = Assert.IsType<Reservation>(getResult.Value);
            Assert.Equal(2, items.hours);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }


        [Fact]
        public async Task GetReservationByID_ShouldReturnReservationByID() {
            Thread.Sleep(4000);
            var TestContext = TodoContextMocker.GetPublicParkContext("GetReservationByID");
            var theController = new ReservationsController(TestContext);

            var result = await theController.GetReservation("ABC4");

            var items = Assert.IsType<Reservation>(result.Value);
            Assert.Equal(DateTime.Parse("22/05/2021 07:00:00"), items.startTime);
        }


        [Fact]
        public async Task PostNoParkingSpotIDReservationAsync_ShouldReturnBadRequest() {
            Thread.Sleep(1000);
            // Arrange
            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            var testContext = TodoContextMocker.GetPublicParkContext(dbName);
            var theController = new ReservationsController(testContext);
            var noParkingSpotID = new Reservation {
                reservationID = "2",
                startTime = DateTime.Parse("2022-05-22 07:00:00"),
                hours = 2,
                endTime = DateTime.Parse("2022-05-22 09:00:00"),
                parkingSpotID = "Z1"
            };

            theController.ModelState.AddModelError("ParkingSpotID", "Required");

            // Act
            var response = await theController.PostReservation(noParkingSpotID);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public async Task DeleteReservation_ShouldDeleteReservation() {
            Thread.Sleep(4500);
            var TestContext = TodoContextMocker.GetPublicParkContext("DeleteReservation");
            var theController = new ReservationsController(TestContext);

            var result = await theController.DeleteReservation("ABC6");

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteNotExistReservation_ShouldReturnNotFound() {
            Thread.Sleep(4600);
            var TestContext = TodoContextMocker.GetPublicParkContext("NotFoundDeleteReservation");
            var theController = new ReservationsController(TestContext);

            var result = await theController.DeleteReservation("1");

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task PostNoStartTImeIDReservationAsync_ShouldReturnBadRequest() {
            Thread.Sleep(300);
            // Arrange
            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            var testContext = TodoContextMocker.GetPublicParkContext(dbName);
            var theController = new ReservationsController(testContext);
            var noParkingLotID = new Reservation {
                reservationID = "2",
                hours = 2,
                endTime = DateTime.Parse("2022-05-22 09:00:00"),
                parkingSpotID = "Z1"
            };

            theController.ModelState.AddModelError("startTime", "Required");

            // Act
            var response = await theController.PostReservation(noParkingLotID);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public async Task PostNoEndTImeIDReservationAsync_ShouldReturnBadRequest() {
            Thread.Sleep(300);
            // Arrange
            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            var testContext = TodoContextMocker.GetPublicParkContext(dbName);
            var theController = new ReservationsController(testContext);
            var noParkingLotID = new Reservation {
                reservationID = "2",
                startTime = DateTime.Parse("2022-05-22 07:00:00"),
                hours = 2,
                parkingSpotID = "Z1"
            };

            theController.ModelState.AddModelError("endTime", "Required");

            // Act
            var response = await theController.PostReservation(noParkingLotID);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }
    }
}
