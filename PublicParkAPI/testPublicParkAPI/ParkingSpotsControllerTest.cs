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

namespace testPublicParkAPI
{
    public class ParkingSpotsControllerTest
    {
        [Fact]
        public async Task GetAllParkingSpotsAsync_ShouldReturnAllCountriesAsync()
        {
            // Arrange
            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            var testContext = TodoContextMocker.GetPublicParkContext(dbName);
            var theController = new ParkingSpotsController(testContext);

            // Act
            var result = await theController.GetParkingSpots();

            // Assert
            var parkingSpots = Assert.IsType<List<ParkingSpot>>(result.Value);
            Assert.Equal(5, parkingSpots.Count());
        }

        [Fact]
        public async Task GetParkingSpotAsync_ShouldReturnNotFound()
        {
            Thread.Sleep(300);
            // Arrange
            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            var testContext = TodoContextMocker.GetPublicParkContext(dbName);
            var theController = new ParkingSpotsController(testContext);
            var testCod = "E21";

            // Act
            var response = await theController.GetParkingSpot(testCod);

            // Assert
            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public async Task GetParkingSpotAsync_ShouldReturnParkingSpot()
        {
            Thread.Sleep(300);
            // Arrange
            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            var testContext = TodoContextMocker.GetPublicParkContext(dbName);
            var theController = new ParkingSpotsController(testContext);
            var testCod = "E1";

            // Act
            var response = await theController.GetParkingSpot(testCod);

            // Assert
            Assert.IsType<ParkingSpot>(response.Value);
        }

        [Fact]
        public async Task GetParkingSpotAsync_ShouldReturnTheRightItemAsync()
        {
            Thread.Sleep(300);
            // Arrange
            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            var testContext = TodoContextMocker.GetPublicParkContext(dbName);
            var theController = new ParkingSpotsController(testContext);
            var testCod = "E1";

            // Act
            var response = await theController.GetParkingSpot(testCod);
            var parkingSpotItem = response.Value;

            // Assert
            Assert.IsType<ParkingSpot>(parkingSpotItem);
            Assert.Equal(testCod, parkingSpotItem.parkingSpotID);
        }

        [Fact]
        public async Task PostNoPriceParkingSpotAsync_ShouldReturnBadRequest()
        {
            Thread.Sleep(300);
            // Arrange
            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            var testContext = TodoContextMocker.GetPublicParkContext(dbName);
            var theController = new ParkingSpotsController(testContext);
            var noPriceParkingSpot = new ParkingSpot
            {
                parkingSpotID="T1", 
                ParkingLotID=1
            };

            theController.ModelState.AddModelError("priceHour", "Required");

            // Act
            var response = await theController.PostParkingSpot(noPriceParkingSpot);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }
        [Fact]
        public async Task PostNoParkingLotIDParkingSpotAsync_ShouldReturnBadRequest()
        {
            Thread.Sleep(300);
            // Arrange
            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            var testContext = TodoContextMocker.GetPublicParkContext(dbName);
            var theController = new ParkingSpotsController(testContext);
            var noParkingLotID = new ParkingSpot
            {
                parkingSpotID = "T1",
                priceHour = 5
            };

            theController.ModelState.AddModelError("ParkingLotID", "Required");

            // Act
            var response = await theController.PostParkingSpot(noParkingLotID);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

    }
}


