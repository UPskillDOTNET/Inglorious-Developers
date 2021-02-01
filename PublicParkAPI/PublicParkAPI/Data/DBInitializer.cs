using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PublicParkAPI.Models;

namespace PublicParkAPI.Data {
    public class DBInitializer
    {
        public static void Initialize(PublicParkContext context)
        {
            context.Database.EnsureCreated();

            if (context.ParkingLots.Any())
            {
                return; //DB is seeded
            }

            // Look for any destinations.
            var parkingLots = new ParkingLot[]
            {
                    new ParkingLot{name="Parque da República", municipality="Vila Nova de Gaia",location="Avenida da República",capacity=125,openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00")},
                    new ParkingLot{name="Parque Brito Capelo", municipality="Matosinhos",location="Rua Brito Capelo",capacity=250,openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00")},
                    new ParkingLot{name="Parque da Liberdade", municipality="Lisboa",location="Avenida da Liberdade",capacity=423,openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00")},
                    new ParkingLot{name="Parque dos Congregados", municipality="Braga",location="Rua dos Congregados",capacity=588,openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00")},
                    new ParkingLot{name="Parque Carlos Alberto", municipality="Porto",location="Praça Carlos Alberto",capacity=365,openingTime= DateTime.Parse("2020-02-22 12:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00")},
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
                    new ParkingSpot{parkingSpotID="A1",priceHour=0.250m, ParkingLotID=1},
                    new ParkingSpot{parkingSpotID="E1",priceHour=0.5m, ParkingLotID=2},
                    new ParkingSpot{parkingSpotID="I1",priceHour=0.9m, ParkingLotID=2},
                    new ParkingSpot{parkingSpotID="O1",priceHour=1.00m, ParkingLotID=3},
                    new ParkingSpot{parkingSpotID="A3",priceHour=0.25m, ParkingLotID=1},
                    new ParkingSpot{parkingSpotID="I5",priceHour=0.25m, ParkingLotID=1},
            };
            foreach (ParkingSpot s in parkingSpots)
            {
                context.ParkingSpots.Add(s);
            }
            context.SaveChanges();

            var reservations = new Reservation[]
            {
                    new Reservation{reservationID="ABC4",startTime= DateTime.Parse("2021-01-31 07:00:00"),hours=1,endTime= DateTime.Parse("2021-05-22 08:00:00"),parkingSpotID="A1",},
                    new Reservation{reservationID="ABC5",startTime= DateTime.Parse("2021-08-22 07:00:00"),hours=2,endTime= DateTime.Parse("2021-08-22 09:00:00"),parkingSpotID="E1",},
                    new Reservation{reservationID="ABC6",startTime= DateTime.Parse("2021-09-22 07:00:00"),hours=12,endTime= DateTime.Parse("2021-09-22 19:00:00"),parkingSpotID="I1"},
                    new Reservation{reservationID="ABC7",startTime= DateTime.Parse("2021-10-22 07:00:00"),hours=3,endTime= DateTime.Parse("2021-10-22 10:00:00"),parkingSpotID="O1",},
                    new Reservation{reservationID="ABC8",startTime= DateTime.Parse("2021-09-22 07:00:00"),hours=1,endTime= DateTime.Parse("2021-09-22 08:00:00"),parkingSpotID="A3",},
                    new Reservation{reservationID="ABC9",startTime= DateTime.Parse("2021-08-22 12:00:00"),hours=1,endTime= DateTime.Parse("2021-08-22 13:00:00"),parkingSpotID="A1",},
                    new Reservation{reservationID="ABC10",startTime= DateTime.Parse("2021-07-22 14:00:00"),hours=1,endTime= DateTime.Parse("2021-07-22 15:00:00"),parkingSpotID="A1",},
                    new Reservation{reservationID="ABC11",startTime= DateTime.Parse("2021-06-22 18:00:00"),hours=1,endTime= DateTime.Parse("2021-06-22 19:00:00"),parkingSpotID="I1",},
                    new Reservation{reservationID="ABC30",startTime= DateTime.Parse("2021-02-01 01:00:00"),hours=22,endTime= DateTime.Parse("2021-02-01 23:00:00"),parkingSpotID="I5",}

            };
            foreach (Reservation r in reservations)
            {
                context.Reservations.Add(r);
            }
            context.SaveChanges();

        }

    }
}
