using Microsoft.EntityFrameworkCore;
using PrivateParkAPI.Data;
using PrivateParkAPI.Models;
using System;



namespace testProject
{
    public static class TodoContextMocker
    {
        private static PrivateParkContext dbContext;

        public static PrivateParkContext GetPrivateParkContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<PrivateParkContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            dbContext = new PrivateParkContext(options);
            Seed();
            return dbContext;
        }

        private static void Seed()
        {
            dbContext.ParkingLots.Add(new ParkingLot { name = "Parque da República", companyOwner = "NorteShopping", location = "Avenida da República", capacity = 125, openingTime = DateTime.Parse("2020-02-22 07:00:00"), closingTime = DateTime.Parse("2999-02-22 19:00:00") });
            dbContext.ParkingLots.Add(new ParkingLot { name = "Parque Brito Capelo", companyOwner = "InRio", location = "Rua Brito Capelo", capacity = 250, openingTime = DateTime.Parse("2020-02-22 07:00:00"), closingTime = DateTime.Parse("2999-02-22 19:00:00") });
            dbContext.ParkingLots.Add(new ParkingLot { name = "Parque da Liberdade", companyOwner = "CasinoEstoril", location = "Avenida da Liberdade", capacity = 423, openingTime = DateTime.Parse("2020-02-22 07:00:00"), closingTime = DateTime.Parse("2999-02-22 19:00:00")});
            dbContext.ParkingLots.Add(new ParkingLot { name = "Parque dos Congregados", companyOwner = "EuSeiLa", location = "Rua dos Congregados", capacity = 588, openingTime = DateTime.Parse("2020-02-22 07:00:00"), closingTime = DateTime.Parse("2999-02-22 19:00:00") });
            dbContext.ParkingLots.Add(new ParkingLot { name = "Parque Carlos Alberto", companyOwner = "Upskill", location = "Praça Carlos Alberto", capacity = 365, openingTime = DateTime.Parse("2020-02-22 12:00:00"), closingTime = DateTime.Parse("2999-02-22 19:00:00") });

            dbContext.ParkingSpots.Add(new ParkingSpot { parkingSpotID = "A1", priceHour = 0.250m, isPrivate = true, floor = 1, parkingLotID = 1 });
            dbContext.ParkingSpots.Add(new ParkingSpot { parkingSpotID = "E1", priceHour = 0.5m, isPrivate = false, floor = 2, parkingLotID = 2 });
            dbContext.ParkingSpots.Add(new ParkingSpot { parkingSpotID = "I1", priceHour = 0.9m, isPrivate = true, floor = 1, parkingLotID = 2 });
            dbContext.ParkingSpots.Add(new ParkingSpot { parkingSpotID = "O1", priceHour = 1.00m, isPrivate = false, parkingLotID = 3 });
            dbContext.ParkingSpots.Add(new ParkingSpot { parkingSpotID = "A3", priceHour = 0.25m, isPrivate = false, parkingLotID = 1 });

            dbContext.Reservations.Add(new Reservation { startTime = DateTime.Parse("2021-05-22 07:00:00"), hours = 1, endTime = DateTime.Parse("2021-05-22 08:00:00"), parkingSpotID = "A1", });
            dbContext.Reservations.Add(new Reservation { startTime = DateTime.Parse("2021-08-22 07:00:00"), hours = 2, endTime = DateTime.Parse("2021-08-22 09:00:00"), parkingSpotID = "E1", });
            dbContext.Reservations.Add(new Reservation { startTime = DateTime.Parse("2021-09-22 07:00:00"), hours = 12, endTime = DateTime.Parse("2021-09-22 19:00:00"), parkingSpotID = "I1" });
            dbContext.Reservations.Add(new Reservation { startTime = DateTime.Parse("2021-10-22 07:00:00"), hours = 3, endTime = DateTime.Parse("2021-10-22 10:00:00"), parkingSpotID = "O1", });
            dbContext.Reservations.Add(new Reservation { startTime = DateTime.Parse("2021-09-22 07:00:00"), hours = 1, endTime = DateTime.Parse("2021-09-22 08:00:00"), parkingSpotID = "A3", });
            dbContext.Reservations.Add(new Reservation { startTime = DateTime.Parse("2021-08-22 12:00:00"), hours = 1, endTime = DateTime.Parse("2021-08-22 13:00:00"), parkingSpotID = "A1", });
            dbContext.Reservations.Add(new Reservation { startTime = DateTime.Parse("2021-07-22 14:00:00"), hours = 1, endTime = DateTime.Parse("2021-07-22 15:00:00"), parkingSpotID = "A1", });
            dbContext.Reservations.Add(new Reservation { startTime = DateTime.Parse("2021-06-22 18:00:00"), hours = 1, endTime = DateTime.Parse("2021-06-22 19:00:00"), parkingSpotID = "I1", });
            dbContext.SaveChanges();
        }
    }
}
