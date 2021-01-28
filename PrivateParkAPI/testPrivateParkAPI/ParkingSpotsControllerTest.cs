using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        public async Task GetAllAsync_ShouldReturnAllAsync()
        {
           
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
            Assert.IsType<NotFoundResult>(result.Result);
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
                isPrivate = true,
                parkingLotID = 1

            };
            // Act
            var result = await theController.PostParkingSpot(parking);


            //Assert
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }





        //        [Fact]
        //        public async Task PostCountryAsync_ShouldCreateAnCountryAsync()
        //        {
        //            // Arrange
        //            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //            var testContext = MockerOMSContext.GetTheOMSContext(dbName);
        //            var theController = new CountryController(testContext);
        //            var theNewCountry = new Country
        //            {
        //                CountryCode = "OTHER",
        //                Name = "Another New Country",
        //                GeoAreaCod = "Z3"
        //            };

        //            // Act
        //            var response = await theController.PostCountry(theNewCountry);
        //            var result = response.Result as CreatedAtActionResult;

        //            // Assert
        //            Assert.NotNull(response);
        //            Assert.IsNotType<BadRequestObjectResult>(result);
        //            Assert.IsType<Country>(result.Value);

        //            var theCountry = result.Value as Country;
        //            Assert.Equal("OTHER", theCountry.CountryCode);
        //        }


        //        [Fact]
        //        public async Task PutNoExistingCountryAsync_ShouldReturnNotFound()
        //        {
        //            // Arrange
        //            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //            var testContext = MockerOMSContext.GetTheOMSContext(dbName);
        //            var theController = new CountryController(testContext);
        //            var testCod = "NoExCod";
        //            var theNonExistingCountry = new Country
        //            {
        //                CountryCode = testCod,
        //                Name = "Updated country " + testCod,
        //                GeoAreaCod = "Z1"
        //            };

        //            // Act
        //            var response = await theController.PutCountry(testCod, theNonExistingCountry);

        //            // Assert
        //            Assert.IsType<NotFoundResult>(response);
        //        }

        //        [Fact]
        //        public async Task PutBadNoNameCountryAsync_ShouldReturnBadRequest()
        //        {
        //            // Arrange
        //            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //            var testContext = MockerOMSContext.GetTheOMSContext(dbName);
        //            var theController = new CountryController(testContext);

        //            var testCod = "C1";
        //            var noNameCountry = new Country
        //            {
        //                CountryCode = testCod,
        //                GeoAreaCod = "Z1"
        //            };

        //            var c = await testContext.FindAsync<Country>(testCod); //To Avoid tracking error
        //            testContext.Entry(c).State = EntityState.Detached;

        //            theController.ModelState.AddModelError("Name", "Required");

        //            // Act
        //            var response = await theController.PutCountry(testCod, noNameCountry);

        //            // Assert
        //            Assert.IsType<BadRequestResult>(response);
        //        }

        //        [Fact]
        //        public async Task PutBadNoGeoAreaCountryAsync_ShouldReturnBadRequest()
        //        {
        //            // Arrange
        //            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //            var testContext = MockerOMSContext.GetTheOMSContext(dbName);
        //            var theController = new CountryController(testContext);

        //            var testCod = "C1";
        //            var noNameCountry = new Country
        //            {
        //                CountryCode = testCod,
        //                Name = "Updated Name"
        //            };

        //            var c = await testContext.FindAsync<Country>(testCod); //To Avoid tracking error
        //            testContext.Entry(c).State = EntityState.Detached;

        //            theController.ModelState.AddModelError("GeoAreaCod", "Required");

        //            // Act
        //            var response = await theController.PutCountry(testCod, noNameCountry);

        //            // Assert
        //            Assert.IsType<BadRequestResult>(response);
        //        }

        //        [Fact]
        //        public async Task PutCountry_ShouldReturnCreatedResponse()
        //        {
        //            // Arrange
        //            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //            var testContext = MockerOMSContext.GetTheOMSContext(dbName);
        //            var theController = new CountryController(testContext);
        //            var testCod = "C1";
        //            var theCountry = new Country
        //            {
        //                CountryCode = testCod,
        //                Name = "Updated Country",
        //                GeoAreaCod = "Z1"
        //            };

        //            var c = await testContext.FindAsync<Country>(testCod); //To Avoid tracking error
        //            testContext.Entry(c).State = EntityState.Detached;

        //            // Act
        //            var response = await theController.PutCountry(testCod, theCountry);

        //            // Assert
        //            Assert.IsType<CreatedAtActionResult>(response);
        //        }

        //        [Fact]
        //        public async Task DeleteNotExistingCountry_ShouldReturnNotFound()
        //        {
        //            // Arrange
        //            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //            var testContext = MockerOMSContext.GetTheOMSContext(dbName);
        //            var theController = new CountryController(testContext);
        //            var testCod = "NoExCod";

        //            // Act
        //            var result = await theController.DeleteCountry(testCod);

        //            // Assert
        //            Assert.IsType<NotFoundResult>(result);
        //        }

        //        [Fact]
        //        public async Task DeleteExistingCountryAsync_ShouldReturnOkResult()
        //        {
        //            // Arrange
        //            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //            var testContext = MockerOMSContext.GetTheOMSContext(dbName);
        //            var theController = new CountryController(testContext);
        //            var testCod = "C1";

        //            // Act
        //            var response = await theController.DeleteCountry(testCod);

        //            // Assert
        //            Assert.IsType<NoContentResult>(response);
        //        }

        //        [Fact]
        //        public async Task DeleteExistingCountryAsync_ShouldRemovetheCountryAsync()
        //        {
        //            // Arrange
        //            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //            var testContext = MockerOMSContext.GetTheOMSContext(dbName);
        //            var theController = new CountryController(testContext);
        //            var testCod = "C1";

        //            // Act
        //            var result = await theController.DeleteCountry(testCod);

        //            var isTheItemThere = await theController.GetCountry(testCod);

        //            // Assert
        //            Assert.IsType<NotFoundResult>(isTheItemThere.Result);
        //        }
    }
}

