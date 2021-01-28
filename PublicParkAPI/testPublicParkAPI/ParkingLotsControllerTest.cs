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

namespace testPublicParkAPI
{
    public class ParkingLotsControllerTest
    {
        //[Fact]
        //public async Task GetAllParkingLotsAsync_ShouldReturnAllParkingLots()
        //{
        //    // Arrange
        //    Thread.Sleep(1000);
        //    var TestContext = TodoContextMocker.GetPublicParkContext("GetAllParkingLots");
        //    var theController = new ParkingLotsController(TestContext);

        //    // Act
        //    var result = await theController.GetParkingLots();

        //    //Assert
        //    var items = Assert.IsType<List<ParkingLot>>(result.Value);
        //    Assert.Equal(5, items.Count);
        //}

        //[Fact]
        //public async Task GetParkingLotByID_ShouldReturnParkingLotByID()
        //{
        //    // Arrange
        //    Thread.Sleep(1000);
        //    var TestContext = TodoContextMocker.GetPublicParkContext("GetParkingLotByID");
        //    var theController = new ParkingLotsController(TestContext);

        //    // Act
        //    var result = await theController.GetParkingLot(3);

        //    //Assert
        //    var items = Assert.IsType<ParkingLot>(result.Value);
        //    Assert.Equal("Parque da Liberdade", items.name);
        //}

        //[Fact]
        //public async Task GetParkingLotByID_ShouldReturnNotFound()
        //{
        //    // Arrange
        //    Thread.Sleep(1000);
        //    var TestContext = TodoContextMocker.GetPublicParkContext("NotFoundParkingLotByID");
        //    var theController = new ParkingLotsController(TestContext);

        //    // Act
        //    var result = await theController.GetParkingLot(0);

        //    //Assert
        //    Assert.IsType<NotFoundResult>(result.Result);
        //}

        //[Fact]
        //public async Task PutParkingLot_ShouldReturnEditedParkingLot()
        //{
        //    // Arrange
        //    Thread.Sleep(1000);
        //    var TestContext = TodoContextMocker.GetPublicParkContext("PutParkingLot");
        //    var theController = new ParkingLotsController(TestContext);

        //    var newParkingLot = new ParkingLot
        //    {
        //        parkingLotID = 1,
        //        name = "Parque da República",
        //        municipality = "Vila Nova de Gaia",
        //        location = "Avenida da República",
        //        capacity = 140,
        //        openingTime = DateTime.Parse("2020-02-22 07:00:00"),
        //        closingTime = DateTime.Parse("2999-02-22 19:00:00")
        //    };

        //    var oldParkingLotResult = await theController.GetParkingLot(1);
        //    var oldParkingLot = oldParkingLotResult.Value;
        //    TestContext.Entry(oldParkingLot).State = EntityState.Detached;

        //    // Act
        //    var result = await theController.PutParkingLot(newParkingLot.parkingLotID, newParkingLot);
        //    var getResult = await theController.GetParkingLot(newParkingLot.parkingLotID);

        //    //Assert
        //    var items = Assert.IsType<ParkingLot>(getResult.Value);
        //    Assert.Equal(140, items.capacity);
        //    Assert.IsType<NoContentResult>(result);
        //}

        //[Fact]
        //public async Task PutNoParkingLot_ShouldReturnNotFound()
        //{
        //    // Arrange
        //    Thread.Sleep(1000);
        //    var TestContext = TodoContextMocker.GetPublicParkContext("NotPutParkingLot");
        //    var theController = new ParkingLotsController(TestContext);

        //    var newParkingLot = new ParkingLot
        //    {
        //        parkingLotID = 0,
        //        name = "Jardim da Pérgola",
        //        municipality = "Porto",
        //        location = "Avenida do Brasil",
        //        capacity = 150,
        //        openingTime = DateTime.Parse("2020-02-22 07:00:00"),
        //        closingTime = DateTime.Parse("2999-02-22 19:00:00")
        //    };

        //    var oldParkingLotResult = await theController.GetParkingLot(1);
        //    var oldParkingLot = oldParkingLotResult.Value;
        //    TestContext.Entry(oldParkingLot).State = EntityState.Detached;

        //    // Act
        //    var getResult = await theController.GetParkingLot(newParkingLot.parkingLotID);

        //    //Assert
        //    Assert.IsType<NotFoundResult>(getResult.Result);
        //}

        //[Fact]
        //public async Task PutNoParkingLot_ShouldReturnBadRequest()
        //{
        //    // Arrange
        //    Thread.Sleep(1000);            
        //    var TestContext = TodoContextMocker.GetPublicParkContext("PutBadRequesParkingLot");
        //    var theController = new ParkingLotsController(TestContext);

        //    var newParkingLot = new ParkingLot
        //    {
        //        parkingLotID = 1,                
        //        municipality = "Vila Nova de Gaia",
        //        closingTime = DateTime.Parse("2999-02-22 19:00:00")
        //    };
        //    theController.ModelState.AddModelError("name", "Required");
        //    var oldParkingLotResult = await TestContext.FindAsync<ParkingLot>(1);
        //    TestContext.Entry(oldParkingLotResult).State = EntityState.Detached;

        //    // Act
        //    var getResult = await theController.PutParkingLot(1, newParkingLot);

        //    //Assert
        //    Assert.IsType<BadRequestObjectResult>(getResult);
        //}

        //[Fact]
        //public async Task PostParkingLot_ShouldCreateNewParkingLot()
        //{
        //    // Arrange
        //    Thread.Sleep(1000);
        //    var TestContext = TodoContextMocker.GetPublicParkContext("PostParkingLot");
        //    var theController = new ParkingLotsController(TestContext);

        //    var newParkingLot = new ParkingLot
        //    {
        //        parkingLotID = 6,
        //        name = "Jardim da Pérgola",
        //        municipality = "Porto",
        //        location = "Avenida do Brasil",
        //        capacity = 150,
        //        openingTime = DateTime.Parse("2020-02-22 07:00:00"),
        //        closingTime = DateTime.Parse("2999-02-22 19:00:00")
        //    };

        //    // Act
        //    var result = await theController.PostParkingLot(newParkingLot);
        //    var getResult = await theController.GetParkingLot(6);

        //    //Assert
        //    var items = Assert.IsType<ParkingLot>(getResult.Value);
        //    Assert.Equal("Jardim da Pérgola", items.name);
        //    Assert.IsType<CreatedAtActionResult>(result.Result);
        //}

        //[Fact]
        //public async Task PostBadParkingLot_ShouldReturnBadRequest()
        //{
        //    // Arrange
        //    Thread.Sleep(1000);
        //    var TestContext = TodoContextMocker.GetPublicParkContext("PostBadRequestParkingLot");
        //    var theController = new ParkingLotsController(TestContext);

        //    var newParkingLot = new ParkingLot
        //    {
        //        parkingLotID = 7,                
        //        openingTime = DateTime.Parse("2020-02-22 07:00:00"),
        //        closingTime = DateTime.Parse("2999-02-22 19:00:00")
        //    };
        //    theController.ModelState.AddModelError("name", "Required");

        //    // Act
        //    var result = await theController.PostParkingLot(newParkingLot);

        //    //Assert
        //    Assert.IsType<BadRequestObjectResult>(result.Result);
        //}

        //[Fact]
        //public async Task DeleteRecomendation_ShouldDeleteRecomendation()
        //{
        //    // Arrange
        //    Thread.Sleep(1500);
        //    var TestContext = TodoContextMocker.GetPublicParkContext("DeleteParkingLot");
        //    var theController = new ParkingLotsController(TestContext);

        //    // Act
        //    var result = await theController.DeleteParkingLot(5);

        //    //Assert
        //    Assert.IsType<NoContentResult>(result);
        //}

        //[Fact]
        //public async Task DeleteNotExistRecomendation_ShouldReturnNotFound()
        //{
        //    // Arrange
        //    Thread.Sleep(1600);
        //    var TestContext = TodoContextMocker.GetPublicParkContext("NotFoundDeleteParkingLot");
        //    var theController = new ParkingLotsController(TestContext);

        //    // Act
        //    var result = await theController.DeleteParkingLot(10);

        //    //Assert
        //    Assert.IsType<NotFoundResult>(result);
        //}
    }
}
