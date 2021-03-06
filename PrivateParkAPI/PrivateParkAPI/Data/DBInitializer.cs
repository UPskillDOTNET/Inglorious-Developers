﻿using PrivateParkAPI.Models;
using System;
using System.Linq;

namespace PrivateParkAPI.Data {
    public class DBInitializer {
        public static void Initialize(PrivateParkContext context) {
            context.Database.EnsureCreated();

            if (context.ParkingLots.Any()) {
                return; //DB is seeded
            }

            // Look for any destinations.
            var parkingLots = new ParkingLot[]
            {
                    new ParkingLot{name="Parque da República", companyOwner="NorteShopping",location="Avenida da República",capacity=125,openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00")},
                    new ParkingLot{name="Parque Brito Capelo",companyOwner="InRio",location="Rua Brito Capelo",capacity=250,openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00")},
                    new ParkingLot{name="Parque da Liberdade", companyOwner="CasinoEstoril",location="Avenida da Liberdade",capacity=423,openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00")},
                    new ParkingLot{name="Parque dos Congregados", companyOwner="EuSeiLa",location="Rua dos Congregados",capacity=588,openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00")},
                    new ParkingLot{name="Parque Carlos Alberto", companyOwner="Upskill",location="Praça Carlos Alberto",capacity=365,openingTime= DateTime.Parse("2020-02-22 12:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00")},
            };
            foreach (ParkingLot p in parkingLots) {
                context.ParkingLots.Add(p);
            }
            context.SaveChanges();


            if (context.ParkingSpots.Any()) {
                return;   // DB has been seeded
            }

            var parkingSpots = new ParkingSpot[]
            {
                    new ParkingSpot{parkingSpotID="A1",priceHour=0.250m,isPrivate=true, floor=1},
                    new ParkingSpot{parkingSpotID="E1",priceHour=0.5m,isPrivate=false, floor=2},
                    new ParkingSpot{parkingSpotID="I1",priceHour=0.9m,isPrivate=true, floor=1},
                    new ParkingSpot{parkingSpotID="O1",priceHour=1.00m,isPrivate=false},
                    new ParkingSpot{parkingSpotID="A3",priceHour=0.25m,isPrivate=false},
            };
            foreach (ParkingSpot s in parkingSpots) {
                context.ParkingSpots.Add(s);
            }
            context.SaveChanges();

            var reservations = new Reservation[]
            {
                    new Reservation{reservationID="ABC1",isCancelled=false,startTime= DateTime.Parse("2021-05-22 07:00:00"),hours=1,endTime= DateTime.Parse("2021-05-22 08:00:00"),parkingSpotID="A1",},
                    new Reservation{reservationID="ABC2",isCancelled=true,startTime= DateTime.Parse("2021-01-31 07:00:00"),hours=2,endTime= DateTime.Parse("2021-08-22 09:00:00"),parkingSpotID="E1",},
                    new Reservation{reservationID="ABC3",isCancelled=false,startTime= DateTime.Parse("2021-09-22 07:00:00"),hours=12,endTime= DateTime.Parse("2021-09-22 19:00:00"),parkingSpotID="I1"},
                    new Reservation{reservationID="ABC4",isCancelled=false,startTime= DateTime.Parse("2021-10-22 07:00:00"),hours=3,endTime= DateTime.Parse("2021-10-22 10:00:00"),parkingSpotID="O1",},
                    new Reservation{reservationID="ABC5",isCancelled=false,startTime= DateTime.Parse("2021-09-22 07:00:00"),hours=1,endTime= DateTime.Parse("2021-09-22 08:00:00"),parkingSpotID="A3",},
                    new Reservation{reservationID="ABC6",isCancelled=false,startTime= DateTime.Parse("2021-08-22 12:00:00"),hours=1,endTime= DateTime.Parse("2021-08-22 13:00:00"),parkingSpotID="A1",},
                    new Reservation{reservationID="ABC7",isCancelled=true,startTime= DateTime.Parse("2021-07-22 14:00:00"),hours=1,endTime= DateTime.Parse("2021-07-22 15:00:00"),parkingSpotID="A1",},
                    new Reservation{reservationID="ABC8",isCancelled=false,startTime= DateTime.Parse("2021-06-22 18:00:00"),hours=1,endTime= DateTime.Parse("2021-06-22 19:00:00"),parkingSpotID="I1",},

            };
            foreach (Reservation r in reservations) {
                context.Reservations.Add(r);
            }
            context.SaveChanges();

        }

    }
}