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
using PublicParkAPI.Repositories;
using AutoMapper;
using PublicParkAPI.Services.Services;
using PublicParkAPI.Mappings;
using PublicParkAPI.DTO;

namespace testPublicParkAPI {
    public class ReservationsControllerTest {

        [Fact]
        public async Task GetAllReservationsAsync_ShouldReturnAllReservationsAsync()
        {
            // Arrange 
            Thread.Sleep(4000);
            var TestContext = TodoContextMocker.GetPublicParkContext("getAllReservations");
            var reservationRepository = new ReservationRepository(TestContext);
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, mapper, reservationRepository);
            var reservationService = new ReservationService(reservationRepository, mapper, parkingSpotRepository);
            var theController = new ReservationsController(reservationService, parkingSpotService);
            //Act
            var result = await theController.GetReservations();

            //Assert
            var items = Assert.IsType<List<ReservationDTO>>(result.Value);
            Assert.Equal(8, items.Count);
        }

        [Fact]
        public async Task GetRecomendationByID_ShouldReturnNotFound()
        {
            Thread.Sleep(4000);
            // Arrange 
            var TestContext = TodoContextMocker.GetPublicParkContext("NotFoundRecomendationByID");
            var reservationRepository = new ReservationRepository(TestContext);
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, mapper, reservationRepository);
            var reservationService = new ReservationService(reservationRepository, mapper, parkingSpotRepository);
            var theController = new ReservationsController(reservationService, parkingSpotService);
            //Act
            var result = await theController.GetReservation("1");
            //Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetReservationByID_ShouldReturnReservationByID()
        {
            Thread.Sleep(4000);
            // Arrange 
            var TestContext = TodoContextMocker.GetPublicParkContext("GetReservationByID");
            var reservationRepository = new ReservationRepository(TestContext);
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, mapper, reservationRepository);
            var reservationService = new ReservationService(reservationRepository, mapper, parkingSpotRepository);
            var theController = new ReservationsController(reservationService, parkingSpotService);
            //Act
            var result = await theController.GetReservation("ABC4");
            //Assert
            var items = Assert.IsType<ReservationDTO>(result.Value);
            Assert.Equal(DateTime.Parse("2021-05-22 07:00:00"), items.startTime);
        }

        [Fact]
        public async Task PostReservation_ShouldCreateNewReservation()
        {
            Thread.Sleep(4000);
            //Arrange
            var TestContext = TodoContextMocker.GetPublicParkContext("PostReservation");
            var reservationRepository = new ReservationRepository(TestContext);
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, mapper, reservationRepository);
            var reservationService = new ReservationService(reservationRepository, mapper, parkingSpotRepository);
            var theController = new ReservationsController(reservationService, parkingSpotService);
            var newReservation = new ReservationDTO
            {
                reservationID = "1",
                startTime = DateTime.Parse("2022-05-22 07:00:00"),
                hours = 2,
                parkingSpotID = "A1"
            };
            //Act
            var result = await theController.PostReservation(newReservation);
            var getResult = await theController.GetReservation("1");
            //Assert
            var items = Assert.IsType<ReservationDTO>(getResult.Value);
            Assert.Equal(2, items.hours);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public async Task PostNoParkingSpotIDReservationAsync_ShouldReturnBadRequest()
        {
            Thread.Sleep(1000);
            // Arrange
            var TestContext = TodoContextMocker.GetPublicParkContext("PostBadReservation");
            var reservationRepository = new ReservationRepository(TestContext);
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, mapper, reservationRepository);
            var reservationService = new ReservationService(reservationRepository, mapper, parkingSpotRepository);
            var theController = new ReservationsController(reservationService, parkingSpotService);
            var noParkingSpotID = new ReservationDTO
            {
                reservationID = "2",
                startTime = DateTime.Parse("2022-05-22 07:00:00"),
                hours = 2,
                
            };

            // Act
            var response = await theController.PostReservation(noParkingSpotID);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }
        [Fact]
        public async Task PatchByID_ShouldReturnNotFound()
        {
            Thread.Sleep(1000);
            // Arrange
            var TestContext = TodoContextMocker.GetPublicParkContext("patch");
            var reservationRepository = new ReservationRepository(TestContext);
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, mapper, reservationRepository);
            var reservationService = new ReservationService(reservationRepository, mapper, parkingSpotRepository);
            var theController = new ReservationsController(reservationService, parkingSpotService);
            var reservationID = "ABC69";

            // Act
            var response = await theController.PatchReservation(reservationID);

            // Assert
            Assert.IsType<NotFoundObjectResult>(response.Result);
        }

        [Fact]
        public async Task PatchByID_ShouldbeCancelled()
        {
            Thread.Sleep(1000);
            // Arrange
            var TestContext = TodoContextMocker.GetPublicParkContext("cancelled");
            var reservationRepository = new ReservationRepository(TestContext);
            var parkingSpotRepository = new ParkingSpotRepository(TestContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Maps>());
            var mapper = config.CreateMapper();
            var parkingSpotService = new ParkingSpotService(parkingSpotRepository, mapper, reservationRepository);
            var reservationService = new ReservationService(reservationRepository, mapper, parkingSpotRepository);
            var theController = new ReservationsController(reservationService, parkingSpotService);
            var reservationID = "ABC4";

            // Act
            var response = await theController.PatchReservation(reservationID);

            // Assert
            Assert.IsType<OkObjectResult>(response.Result);
        }

    }
}
