using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicParkAPI.Controllers;
using PublicParkAPI.Data;
using PublicParkAPI.DTO;
using PublicParkAPI.Mappings;
using PublicParkAPI.Models;
using PublicParkAPI.Repositories;
using PublicParkAPI.Services.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace testPublicParkAPI
{
    public class ReservationsControllerTest
    {

        [Fact]
        public async Task GetAllReservationsAsync_ShouldReturnAllReservationsAsync()
        {
            // Arrange
            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            var TestContext = PublicPark_ParkingReservationContext.GetPublicParkContext(dbName);
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
            // Arrange 
            var TestContext = PublicPark_ParkingReservationContext.GetPublicParkContext("NotFoundRecomendationByID");
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
            // Arrange 
            var TestContext = PublicPark_ParkingReservationContext.GetPublicParkContext("GetReservationByID");
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
            //Arrange
            var TestContext = PublicPark_ParkingReservationContext.GetPublicParkContext("PostReservation");
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
            // Arrange
            var TestContext = PublicPark_ParkingReservationContext.GetPublicParkContext("PostBadReservation");
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
            // Arrange
            var TestContext = PublicPark_ParkingReservationContext.GetPublicParkContext("patch");
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
            // Arrange
            var TestContext = PublicPark_ParkingReservationContext.GetPublicParkContext("cancelled");
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

    public static class PublicPark_ParkingReservationContext
    {
        private static PublicParkContext publicReservationsContext;
        public static PublicParkContext GetPublicParkContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<PublicParkContext>()
                            .UseInMemoryDatabase(databaseName: dbName)
                            .Options;

            publicReservationsContext = new PublicParkContext(options);
            Seed();
            return publicReservationsContext;
        }

        private static void Seed()
        {
            publicReservationsContext.ParkingSpots.Add(new ParkingSpot { parkingSpotID = "A1", priceHour = 0.250m, ParkingLotID = 1 });
            publicReservationsContext.ParkingSpots.Add(new ParkingSpot { parkingSpotID = "E1", priceHour = 0.5m, ParkingLotID = 2 });
            publicReservationsContext.ParkingSpots.Add(new ParkingSpot { parkingSpotID = "I1", priceHour = 0.9m, ParkingLotID = 2 });
            publicReservationsContext.ParkingSpots.Add(new ParkingSpot { parkingSpotID = "O1", priceHour = 1.00m, ParkingLotID = 3 });
            publicReservationsContext.ParkingSpots.Add(new ParkingSpot { parkingSpotID = "A3", priceHour = 0.25m, ParkingLotID = 1 });

            publicReservationsContext.ParkingLots.Add(new ParkingLot { name = "Parque da República", municipality = "Vila Nova de Gaia", location = "Avenida da República", capacity = 125, openingTime = DateTime.Parse("2020-02-22 07:00:00"), closingTime = DateTime.Parse("2999-02-22 19:00:00") });
            publicReservationsContext.ParkingLots.Add(new ParkingLot { name = "Parque Brito Capelo", municipality = "Matosinhos", location = "Rua Brito Capelo", capacity = 250, openingTime = DateTime.Parse("2020-02-22 07:00:00"), closingTime = DateTime.Parse("2999-02-22 19:00:00") });
            publicReservationsContext.ParkingLots.Add(new ParkingLot { name = "Parque da Liberdade", municipality = "Lisboa", location = "Avenida da Liberdade", capacity = 423, openingTime = DateTime.Parse("2020-02-22 07:00:00"), closingTime = DateTime.Parse("2999-02-22 19:00:00") });
            publicReservationsContext.ParkingLots.Add(new ParkingLot { name = "Parque dos Congregados", municipality = "Braga", location = "Rua dos Congregados", capacity = 588, openingTime = DateTime.Parse("2020-02-22 07:00:00"), closingTime = DateTime.Parse("2999-02-22 19:00:00") });
            publicReservationsContext.ParkingLots.Add(new ParkingLot { name = "Parque Carlos Alberto", municipality = "Porto", location = "Praça Carlos Alberto", capacity = 365, openingTime = DateTime.Parse("2020-02-22 12:00:00"), closingTime = DateTime.Parse("2999-02-22 19:00:00") });

            publicReservationsContext.Reservations.Add(new Reservation { reservationID = "ABC4", startTime = DateTime.Parse("2021-05-22 07:00:00"), hours = 1, endTime = DateTime.Parse("2021-05-22 08:00:00"), parkingSpotID = "A1" });
            publicReservationsContext.Reservations.Add(new Reservation { reservationID = "ABC5", startTime = DateTime.Parse("2021-08-22 07:00:00"), hours = 2, endTime = DateTime.Parse("2021-08-22 09:00:00"), parkingSpotID = "E1" });
            publicReservationsContext.Reservations.Add(new Reservation { reservationID = "ABC6", startTime = DateTime.Parse("2021-09-22 07:00:00"), hours = 12, endTime = DateTime.Parse("2021-09-22 19:00:00"), parkingSpotID = "I1" });
            publicReservationsContext.Reservations.Add(new Reservation { reservationID = "ABC7", startTime = DateTime.Parse("2021-10-22 07:00:00"), hours = 3, endTime = DateTime.Parse("2021-10-22 10:00:00"), parkingSpotID = "O1" });
            publicReservationsContext.Reservations.Add(new Reservation { reservationID = "ABC8", startTime = DateTime.Parse("2021-09-22 07:00:00"), hours = 1, endTime = DateTime.Parse("2021-09-22 08:00:00"), parkingSpotID = "A3" });
            publicReservationsContext.Reservations.Add(new Reservation { reservationID = "ABC9", startTime = DateTime.Parse("2021-08-22 12:00:00"), hours = 1, endTime = DateTime.Parse("2021-08-22 13:00:00"), parkingSpotID = "A1" });
            publicReservationsContext.Reservations.Add(new Reservation { reservationID = "ABC10", startTime = DateTime.Parse("2021-07-22 14:00:00"), hours = 1, endTime = DateTime.Parse("2021-07-22 15:00:00"), parkingSpotID = "A1" });
            publicReservationsContext.Reservations.Add(new Reservation { reservationID = "ABC11", startTime = DateTime.Parse("2021-06-22 18:00:00"), hours = 1, endTime = DateTime.Parse("2021-06-22 19:00:00"), parkingSpotID = "I1" });


            publicReservationsContext.SaveChanges();
        }
    }
}
