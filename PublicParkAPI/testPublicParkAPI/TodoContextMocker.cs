using Microsoft.EntityFrameworkCore;
using PublicParkAPI.Data;
using PublicParkAPI.Models;
using System;



namespace testProject
{
    public static class TodoContextMocker
    {
        private static PublicParkContext dbContext;

        public static PublicParkContext GetPublicParkContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<PublicParkContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            dbContext = new PublicParkContext(options);
            Seed();
            return dbContext;
        }

        private static void Seed()
        {
            dbContext.ParkingLots.Add(new ParkingLot {name = "Parque da República", municipality = "Vila Nova de Gaia", location = "Avenida da República", capacity = 125, openingTime = DateTime.Parse("2020-02-22 07:00:00"), closingTime = DateTime.Parse("2999-02-22 19:00:00") });
            dbContext.ParkingLots.Add(new ParkingLot {name = "Parque Brito Capelo", municipality = "Matosinhos", location = "Rua Brito Capelo", capacity = 250, openingTime = DateTime.Parse("2020-02-22 07:00:00"), closingTime = DateTime.Parse("2999-02-22 19:00:00") });
            dbContext.ParkingLots.Add(new ParkingLot {name = "Parque da Liberdade", municipality = "Lisboa", location = "Avenida da Liberdade", capacity = 423, openingTime = DateTime.Parse("2020-02-22 07:00:00"), closingTime = DateTime.Parse("2999-02-22 19:00:00")  });
            dbContext.ParkingLots.Add(new ParkingLot {name = "Parque dos Congregados", municipality = "Braga", location = "Rua dos Congregados", capacity = 588, openingTime = DateTime.Parse("2020-02-22 07:00:00"), closingTime = DateTime.Parse("2999-02-22 19:00:00") });
            dbContext.ParkingLots.Add(new ParkingLot {name = "Parque Carlos Alberto", municipality = "Porto", location = "Praça Carlos Alberto", capacity = 365, openingTime = DateTime.Parse("2020-02-22 12:00:00"), closingTime = DateTime.Parse("2999-02-22 19:00:00") });

            dbContext.ParkingSpots.Add(new ParkingSpot{parkingSpotID="A1",priceHour=0.250m, ParkingLotID=1});
            dbContext.ParkingSpots.Add(new ParkingSpot{parkingSpotID="E1",priceHour=0.5m, ParkingLotID=2});
            dbContext.ParkingSpots.Add(new ParkingSpot{parkingSpotID="I1",priceHour=0.9m, ParkingLotID=2});
            dbContext.ParkingSpots.Add(new ParkingSpot{parkingSpotID="O1",priceHour=1.00m, ParkingLotID=3});
            dbContext.ParkingSpots.Add(new ParkingSpot{parkingSpotID="A3",priceHour=0.25m, ParkingLotID=1});

            dbContext.Reservations.Add(new Reservation{startTime= DateTime.Parse("2021-05-22 07:00:00"),hours=1,endTime= DateTime.Parse("2021-05-22 08:00:00"),parkingSpotID="A1"});
            dbContext.Reservations.Add(new Reservation{startTime= DateTime.Parse("2021-08-22 07:00:00"),hours=2,endTime= DateTime.Parse("2021-08-22 09:00:00"),parkingSpotID="E1"});
            dbContext.Reservations.Add(new Reservation{startTime= DateTime.Parse("2021-09-22 07:00:00"),hours=12,endTime= DateTime.Parse("2021-09-22 19:00:00"),parkingSpotID="I1"});
            dbContext.Reservations.Add(new Reservation{startTime= DateTime.Parse("2021-10-22 07:00:00"),hours=3,endTime= DateTime.Parse("2021-10-22 10:00:00"),parkingSpotID="O1"});
            dbContext.Reservations.Add(new Reservation{startTime= DateTime.Parse("2021-09-22 07:00:00"),hours=1,endTime= DateTime.Parse("2021-09-22 08:00:00"),parkingSpotID="A3"});
            dbContext.Reservations.Add(new Reservation{startTime= DateTime.Parse("2021-08-22 12:00:00"),hours=1,endTime= DateTime.Parse("2021-08-22 13:00:00"),parkingSpotID="A1"});
            dbContext.Reservations.Add(new Reservation{startTime= DateTime.Parse("2021-07-22 14:00:00"),hours=1,endTime= DateTime.Parse("2021-07-22 15:00:00"),parkingSpotID="A1"});
            dbContext.Reservations.Add(new Reservation{startTime= DateTime.Parse("2021-06-22 18:00:00"),hours=1,endTime= DateTime.Parse("2021-06-22 19:00:00"),parkingSpotID="I1"});
            dbContext.SaveChanges();
        }
    }
}
