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
                    new ParkingLot{name="Avenida da República", municipality="Vila Nova de Gaia",location="Avenida da República",capacity=125,openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00")},
                    new ParkingLot{name="Rua Brito Capelo", municipality="Matosinhos",location="Rua Brito Capelo",capacity=250,openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00")},
                    new ParkingLot{name="Avenida da Liberdade", municipality="Lisboa",location="Avenida da Liberdade",capacity=423,openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00")},
                    new ParkingLot{name="Rua dos Congregados", municipality="Braga",location="Rua dos Congregados",capacity=588,openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00")},
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
            };
            foreach (ParkingSpot s in parkingSpots)
            {
                context.ParkingSpots.Add(s);
            }
            context.SaveChanges();

            var reservations = new Reservation[]
            {
                    new Reservation{startTime= DateTime.Parse("2021-05-22 07:00:00"),hours=1,endTime= DateTime.Parse("2021-05-22 08:00:00"),parkingSpotID="A1",parkingLotID=1},
                    new Reservation{startTime= DateTime.Parse("2021-08-22 07:00:00"),hours=2,endTime= DateTime.Parse("2021-08-22 09:00:00"),parkingSpotID="E1",parkingLotID=2},
                    new Reservation{startTime= DateTime.Parse("2021-09-22 07:00:00"),hours=12,endTime= DateTime.Parse("2021-09-22 19:00:00"),parkingSpotID="I1",parkingLotID=3},
                    new Reservation{startTime= DateTime.Parse("2021-10-22 07:00:00"),hours=3,endTime= DateTime.Parse("2021-10-22 10:00:00"),parkingSpotID="O1",parkingLotID=4},
                    new Reservation{startTime= DateTime.Parse("2021-09-22 07:00:00"),hours=1,endTime= DateTime.Parse("2021-09-22 08:00:00"),parkingSpotID="A3",parkingLotID=1},
                    new Reservation{startTime= DateTime.Parse("2021-08-22 12:00:00"),hours=1,endTime= DateTime.Parse("2021-08-22 13:00:00"),parkingSpotID="A1",parkingLotID=1},
                    new Reservation{startTime= DateTime.Parse("2021-07-22 14:00:00"),hours=1,endTime= DateTime.Parse("2021-07-22 15:00:00"),parkingSpotID="A1",parkingLotID=1},
                    new Reservation{startTime= DateTime.Parse("2021-06-22 18:00:00"),hours=1,endTime= DateTime.Parse("2021-06-22 19:00:00"),parkingSpotID="I1",parkingLotID=2},

            };
            foreach (Reservation r in reservations)
            {
                context.Reservations.Add(r);
            }
            context.SaveChanges();

        }

    }
}
