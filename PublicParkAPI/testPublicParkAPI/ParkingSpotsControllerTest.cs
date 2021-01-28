﻿using System;
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
        public async Task GetAllParkingSpotsAsync_ShouldReturnAllParkingSpotsAsync()
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
                parkingSpotID = "TEste",
                priceHour = 5
            };

            theController.ModelState.AddModelError("ParkingLotID", "Required");

            // Act
            var response = await theController.PostParkingSpot(noParkingLotID);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public async Task PostParkingSpotAsync_ShouldReturnCreatedResponse()
        {
            Thread.Sleep(300);
            // Arrange
            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            var testContext = TodoContextMocker.GetPublicParkContext(dbName);
            var theController = new ParkingSpotsController(testContext);
            var theNewParkingSpot = new ParkingSpot
            {
                parkingSpotID = "T13",
                priceHour = 0.9m,
                ParkingLotID = 2
            };

            // Act
            var response = await theController.PostParkingSpot(theNewParkingSpot);

            // Assert
            Assert.IsType<CreatedAtActionResult>(response.Result);
        }

        [Fact]
        public async Task PostParkingSpotAsync_ShouldCreateAnCountryAsync()
        {
            // Arrange
            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            var testContext = TodoContextMocker.GetPublicParkContext(dbName);
            var theController = new ParkingSpotsController(testContext);
            var thenewParkingSpot = new ParkingSpot
            {
                parkingSpotID = "T132",
                priceHour = 0.9m,
                ParkingLotID = 2
            };

            // Act
            var response = await theController.PostParkingSpot(thenewParkingSpot);
            var result = response.Result as CreatedAtActionResult;

            // Assert
            Assert.NotNull(response);
            Assert.IsNotType<BadRequestObjectResult>(result);
            Assert.IsType<ParkingSpot>(result.Value);

            var theParkingSpot = result.Value as ParkingSpot;
            Assert.Equal("T132", theParkingSpot.parkingSpotID);
        }

        [Fact]
        public async Task PutNoExistingParkingSpotAsync_ShouldReturnNotFound()
        {
            // Arrange
            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            var testContext = TodoContextMocker.GetPublicParkContext(dbName);
            var theController = new ParkingSpotsController(testContext);
            var testCod = "NoExCod";

            var theNonParkingSpot = new ParkingSpot
            {
                parkingSpotID = testCod,
                priceHour = 0.9m,
                ParkingLotID = 2
            };

            // Act
            var response = await theController.PutParkingSpot(testCod, theNonParkingSpot);

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }

        //[Fact]
        //public async Task PutBadNoNameParkingSpot_ShouldReturnBadRequest()
        //{
        //    // Arrange
        //    var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //    var testContext = TodoContextMocker.GetPublicParkContext(dbName);
        //    var theController = new ParkingSpotsController(testContext);
        //    var testCod = "NoExCod";

        //}



    }
}


