using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrivateParkAPI.Models;

namespace PrivateParkAPI.Data
{
    public class DBInitializer
    {
        public static void Initialize(PrivateParkContext context)
        {
            context.Database.EnsureCreated();

            if (context.ParkingLots.Any())
            {
                return; //DB is seeded
            }

            // Look for any destinations.
            var parkingLots = new ParkingLot[]
            {
                    new ParkingLot{name="Parque de Estacionamento do Palácio", companyOwner="NorteShopping", location="Rua Professor Henrique de Barros", capacity=35, openingTime= DateTime.Parse("2021-07-26 08:30:00"), closingTime= DateTime.Parse("2021-02-22 19:00:00")},
                    new ParkingLot{name="Parque de Estacionamento do Silo Auto", companyOwner="Arrábida Shopping", location="Avenida dos Estados Unidos da América", capacity=142, openingTime= DateTime.Parse("2021-09-12 12:00:00"), closingTime= DateTime.Parse("2021-22 19:00:00")},
                    new ParkingLot{name="Parque de Estacionamento de S.Roque", companyOwner="Dolce Vita", location="Rua Francisco Lopes Ferraz", capacity=213, openingTime= DateTime.Parse("2021-02-22 10:00:00"), closingTime= DateTime.Parse("2021-02-22 19:00:00")},
                    new ParkingLot{name="Parque de Estacionamento Alfândega", companyOwner="Alameda Shooping", location="Avenida da Liberdade", capacity=489, openingTime= DateTime.Parse("2021-04-22 07:00:00"), closingTime= DateTime.Parse("2021-02-22 19:00:00")},
                    new ParkingLot{name="Parque de Estacionamento Trindade", companyOwner="Espaço Guimarães", location="Rua de Ceuta", capacity=218, openingTime= DateTime.Parse("2021-02-23 11:00:00"), closingTime= DateTime.Parse("2021-02-22 19:00:00")},
            };
            foreach (ParkingLot p in parkingLots)
            {
                context.ParkingLots.Add(p);
            }
            context.SaveChanges();


            if (context.ParkingSpots.Any())
            {
                return;   // DB has been seeded
            }

            var parkingSpots = new ParkingSpot[]
            {
                    new ParkingSpot{parkingSpotID="2D",priceHour=0.250m, parkingLotID=2},
                    new ParkingSpot{parkingSpotID="6W",priceHour=0.5m, parkingLotID=1},
                    new ParkingSpot{parkingSpotID="1A",priceHour=0.9m, parkingLotID=1},
                    new ParkingSpot{parkingSpotID="3F",priceHour=1.00m, parkingLotID=2},
                    new ParkingSpot{parkingSpotID="8T",priceHour=0.25m, parkingLotID=3},
            };
            foreach (ParkingSpot s in parkingSpots)
            {
                context.ParkingSpots.Add(s);
            }
            context.SaveChanges();

            var reservations = new Reservation[]
            {
                    new Reservation{startTime= DateTime.Parse("2021-03-22 07:00:00"),hours=1,endTime= DateTime.Parse("2021-03-25 08:00:00"),parkingSpotID="A1",parkingLotID=1},
                    new Reservation{startTime= DateTime.Parse("2021-04-11 09:00:00"),hours=2,endTime= DateTime.Parse("2021-08-22 09:00:00"),parkingSpotID="E1",parkingLotID=2},
                    new Reservation{startTime= DateTime.Parse("2021-09-22 07:00:00"),hours=12,endTime= DateTime.Parse("2021-10-21 19:00:00"),parkingSpotID="I1",parkingLotID=3},
                    new Reservation{startTime= DateTime.Parse("2021-08-22 07:00:00"),hours=3,endTime= DateTime.Parse("2021-10-12 10:00:00"),parkingSpotID="O1",parkingLotID=4},
                    new Reservation{startTime= DateTime.Parse("2021-10-21 09:00:00"),hours=1,endTime= DateTime.Parse("2021-10-27 08:00:00"),parkingSpotID="A3",parkingLotID=1},
                    new Reservation{startTime= DateTime.Parse("2021-08-23 13:00:00"),hours=1,endTime= DateTime.Parse("2021-08-29 19:00:00"),parkingSpotID="A1",parkingLotID=1},
                    new Reservation{startTime= DateTime.Parse("2021-10-22 18:00:00"),hours=1,endTime= DateTime.Parse("2021-10-22 15:00:00"),parkingSpotID="A1",parkingLotID=1},
                    new Reservation{startTime= DateTime.Parse("2021-06-23 19:00:00"),hours=1,endTime= DateTime.Parse("2021-06-24 19:00:00"),parkingSpotID="I1",parkingLotID=2},

            };
            foreach (Reservation r in reservations)
            {
                context.Reservations.Add(r);
            }
            context.SaveChanges();

        }

    }
}