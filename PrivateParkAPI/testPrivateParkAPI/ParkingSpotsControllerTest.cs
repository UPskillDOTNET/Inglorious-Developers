using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PrivateParkAPI.Controllers;
using PrivateParkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using testProject;
using Xunit;

namespace testPrivateParkAPI
{
    public class ParkingSpotsControllerTest
    {
        [Fact]
        public async Task GetAllnotPrivateAsync_ShouldReturnAllnotPrivateAsync()
        {
            
            // Arrange 
            var TestContext = TodoContextMocker.GetPrivateParkContext("GetAllnotPrivateParkingSpots");
            var theController = new ParkingSpotsController(TestContext);
            // Act
            var result = await theController.GetnotPrivateParkingSpots();
            //Assert
            var items = Assert.IsType<List<ParkingSpot>>(result.Value);
            Assert.Equal(3, items.Count);
        }
        [Fact]
        public async Task GetAllAvailableAsync_ShouldReturnAllAvailableAsync()
        {

            // Arrange 
            var TestContext = TodoContextMocker.GetPrivateParkContext("GetAllAvailableParkingSpots");
            var theController = new ParkingSpotsController(TestContext);
            // Act
            var result = await theController.GetParkingFreeSpots();
            //Assert
            var items = Assert.IsType<List<ParkingSpot>>(result.Value);
            Assert.Equal(4, items.Count);
        }
        [Fact]
        public async Task GetAllAvailableinHourAsync_ShouldReturnAllAvailableInHourAsync()
        {

            // Arrange 
            var TestContext = TodoContextMocker.GetPrivateParkContext("GetAllinHourParkingSpots");
            var theController = new ParkingSpotsController(TestContext);
            // Act
            var result = await theController.GetParkingSpecificFreeSpots(DateTime.Parse("2021 - 08 - 22 07:00:00"),DateTime.Parse("2021 - 09 - 22 19:00:00"));
            //Assert
            var items = Assert.IsType<List<ParkingSpot>>(result.Value);
            Assert.Equal(1, items.Count);
        }
        [Fact]
        public async Task GetAllAsync_ShouldReturnAllAsync()
        {
            Thread.Sleep(4000);
            // Arrange 
            var TestContext = TodoContextMocker.GetPrivateParkContext("GetAllParkingSpots");
            var theController = new ParkingSpotsController(TestContext);
            // Act
            var result = await theController.GetAllParkingSpots();
            //Assert
            var items = Assert.IsType<List<ParkingSpot>>(result.Value);
            Assert.Equal(5, items.Count);
        }

        [Fact]
        public async Task GetParkingSpotAsync_ShouldReturnNotFound()
        {
            
            // Arrange 
            var TestContext = TodoContextMocker.GetPrivateParkContext("notGetParkingSpot");
            var theController = new ParkingSpotsController(TestContext);
            var testCod = "A5";
            // Act
            var result = await theController.GetParkingSpot(testCod);
            //Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetParkingSpotAsync_ShouldReturnParkingSpot()
        {

            // Arrange 
            var TestContext = TodoContextMocker.GetPrivateParkContext("GetParkingSpot");
            var theController = new ParkingSpotsController(TestContext);
            var testCod = "E1";
            // Act
            var result = await theController.GetParkingSpot(testCod);
            //Assert
             Assert.IsType<ParkingSpot>(result.Value);
        }


        [Fact]
        public async Task GetTheRightParkingSpotAsync_ShouldReturnTheRightParkingSpot()
        {

            // Arrange 
            var TestContext = TodoContextMocker.GetPrivateParkContext("GetrightParkingSpot");
            var theController = new ParkingSpotsController(TestContext);
            var testCod = "E1";
            // Act
            var result = await theController.GetParkingSpot(testCod);
            var parkingSpotItem = result.Value;

            //Assert
            Assert.IsType<ParkingSpot>(result.Value);
            Assert.Equal(testCod, parkingSpotItem.parkingSpotID);
        }


        [Fact]
        public async Task postnoPriceasync_shouldreturnbadrequest()
        {

            // Arrange 
            var TestContext = TodoContextMocker.GetPrivateParkContext("postbadParkingSpot");
            var theController = new ParkingSpotsController(TestContext);
            var nopricespot = new ParkingSpot
            {
                parkingSpotID = "A5",
                floor = 1,
                isCovered = false,
                isPrivate = true,
                parkingLotID = 1
            };
            theController.ModelState.AddModelError("priceHour", "required");
            // Act
            var result = await theController.PostParkingSpot(nopricespot);
           

            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task postnoParkingLotIDasync_shouldreturnbadrequest()
        {

            // Arrange 
            var TestContext = TodoContextMocker.GetPrivateParkContext("postbadParkingSpot2");
            var theController = new ParkingSpotsController(TestContext);
            var noLotIDspot = new ParkingSpot
            {
                parkingSpotID = "A5",
                priceHour = 1.30M,
                floor = 1,
                isCovered = false,
                isPrivate = true,
               
            };
            theController.ModelState.AddModelError("parkingLotID", "required");
            // Act
            var result = await theController.PostParkingSpot(noLotIDspot);


            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task PostValidParkingSlotasync_ShouldReturnCreatedResponse()
        {

            // Arrange 
            var TestContext = TodoContextMocker.GetPrivateParkContext("postvalidParkingSpot");
            var theController = new ParkingSpotsController(TestContext);
            var parking = new ParkingSpot
            {
                parkingSpotID = "A5",
                priceHour = 1.30M,
                floor = 1,
                isCovered = false,
                isPrivate = true,
                parkingLotID = 1

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
            var TestContext = TodoContextMocker.GetPrivateParkContext("postParkingSpotisParkingSpot");
            var theController = new ParkingSpotsController(TestContext);
            var parkingID = "A6";
            var parking = new ParkingSpot
            {
                parkingSpotID = "A6",
                priceHour = 1.50M,
                floor = 0,
                isCovered = false,
                isPrivate = false,
                parkingLotID = 2

            };

            // Act

            var result = await theController.PostParkingSpot(parking);
            var response = await theController.GetParkingSpot(parkingID);

            // Assert
            Assert.NotNull(response);
            Assert.IsNotType<BadRequestObjectResult>(response);
            Assert.IsType<ParkingSpot>(response.Value);

            Assert.Equal(0, response.Value.floor);
        }


        [Fact]
        public async Task PutNoExistingParkingSpotAsync_ShouldReturnNotFound()
        {
            // Arrange
            var TestContext = TodoContextMocker.GetPrivateParkContext("postParkingSpotnotFound");
            var theController = new ParkingSpotsController(TestContext);

            var parking = new ParkingSpot
            {
                parkingSpotID = "A36",
                priceHour = 1.50M,
                floor = 0,
                isCovered = true,
                isPrivate = false,
                parkingLotID = 2

            };

            // Act
            var response = await theController.PutParkingSpot("A36", parking);

            // Assert
            Assert.IsType<NotFoundObjectResult>(response);
        }



        [Fact]
        public async Task PutNofloorAsync_ShouldReturnBadRequest()
        {
            // Arrange
            var TestContext = TodoContextMocker.GetPrivateParkContext("putParkingBadRequest");
            var theController = new ParkingSpotsController(TestContext);

            var parkingID = "E1";
            var parking = new ParkingSpot
            {
               parkingSpotID = "E1",
               priceHour = 1.50M,
               isCovered = true,
               isPrivate = true,
               parkingLotID =1
               
            };

            var c = await TestContext.FindAsync<ParkingSpot>(parkingID); //To Avoid tracking error
            TestContext.Entry(c).State = EntityState.Detached;

            theController.ModelState.AddModelError("floor", "Required");

            // Act
            var response = await theController.PutParkingSpot(parkingID, parking);

            // Assert
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async Task PutNoPriceAsync_ShouldReturnBadRequest()
        {
            // Arrange
            var TestContext = TodoContextMocker.GetPrivateParkContext("putParkingBadRequest2");
            var theController = new ParkingSpotsController(TestContext);

            var parkingID = "A1";
            var parking = new ParkingSpot
            {
                parkingSpotID = "A1",
                floor = 1,
                isCovered = false,
                isPrivate = true,
                parkingLotID = 1

            };

            var c = await TestContext.FindAsync<ParkingSpot>(parkingID); //To Avoid tracking error
            TestContext.Entry(c).State = EntityState.Detached;

            theController.ModelState.AddModelError("priceHour", "Required");

            // Act
            var response = await theController.PutParkingSpot(parkingID, parking);

            // Assert
            Assert.IsType<BadRequestResult>(response);
        }


        [Fact]
        public async Task PutParkingSpot_ShouldReturnCreatedResponse()
        {
            // Arrange
            var TestContext = TodoContextMocker.GetPrivateParkContext("putParkingisParking");
            var theController = new ParkingSpotsController(TestContext);
            var parkingID = "E1";
            var parking = new ParkingSpot
            {
                parkingSpotID = "E1",
                priceHour = 1.75M,
                floor = 1,
                isCovered = true,
                isPrivate = true,
                parkingLotID = 1

            };

            var c = await TestContext.FindAsync<ParkingSpot>(parkingID); //To Avoid tracking error
            TestContext.Entry(c).State = EntityState.Detached;

            // Act
            var response = await theController.PutParkingSpot(parkingID, parking);

            // Assert
            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public async Task DeleteNotExistingParkingSpot_ShouldReturnNotFound()
        {
            // Arrange
            var TestContext = TodoContextMocker.GetPrivateParkContext("deleteParkingNotFound");
            var theController = new ParkingSpotsController(TestContext);
            var parkingID = "E7";

            // Act
            var result = await theController.DeleteParkingSpot(parkingID);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteExistingParkingSpotAsync_ShouldReturnOkResult()
        {
            // Arrange
            var TestContext = TodoContextMocker.GetPrivateParkContext("deleteParkingOK");
            var theController = new ParkingSpotsController(TestContext);
            var parkingID = "E1";

            // Act
            var response = await theController.DeleteParkingSpot(parkingID);

            // Assert
            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public async Task DeleteExistingParkingSpotAsync_ShouldRemovetheParkingSpotAsync()
        {
            // Arrange
            var TestContext = TodoContextMocker.GetPrivateParkContext("deletedParkIsDeleted");
            var theController = new ParkingSpotsController(TestContext);
            var parkingID = "A1";

            // Act
            var response = await theController.DeleteParkingSpot(parkingID);

            var notExists = await theController.GetParkingSpot(parkingID);

            // Assert
            Assert.IsType<NotFoundObjectResult>(notExists.Result);
        }
    }
}

