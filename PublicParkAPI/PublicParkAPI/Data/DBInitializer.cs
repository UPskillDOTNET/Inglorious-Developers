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

            if (context.ParkingSpots.Any())
            {
                return;   // DB has been seeded
            }

            var parkingSpots = new ParkingSpot[]
            {
                    new ParkingSpot{parkingSpotID="A1",priceHour=0.250m},
                    new ParkingSpot{parkingSpotID="E1",priceHour=0.5m},
                    new ParkingSpot{parkingSpotID="I1",priceHour=0.9m},
                    new ParkingSpot{parkingSpotID="O1",priceHour=1.00m},
                    new ParkingSpot{parkingSpotID="A3",priceHour=0.25m},
                    new ParkingSpot{parkingSpotID="I5",priceHour=0.25m},
            };
            foreach (ParkingSpot s in parkingSpots)
            {
                context.ParkingSpots.Add(s);
            }
            context.SaveChanges();

            var reservations = new Reservation[]
            {
                    new Reservation{reservationID="ABC4",startTime= DateTime.Parse("2021-01-31 07:00:00"),hours=1,endTime= DateTime.Parse("2021-05-22 08:00:00"),parkingSpotID="A1",isCancelled=false},
                    new Reservation{reservationID="ABC5",startTime= DateTime.Parse("2021-08-22 07:00:00"),hours=2,endTime= DateTime.Parse("2021-08-22 09:00:00"),parkingSpotID="E1",isCancelled=false},
                    new Reservation{reservationID="ABC6",startTime= DateTime.Parse("2021-09-22 07:00:00"),hours=12,endTime= DateTime.Parse("2021-09-22 19:00:00"),parkingSpotID="I1",isCancelled=false},
                    new Reservation{reservationID="ABC7",startTime= DateTime.Parse("2021-10-22 07:00:00"),hours=3,endTime= DateTime.Parse("2021-10-22 10:00:00"),parkingSpotID="O1",isCancelled=false},
                    new Reservation{reservationID="ABC8",startTime= DateTime.Parse("2021-09-22 07:00:00"),hours=1,endTime= DateTime.Parse("2021-09-22 08:00:00"),parkingSpotID="A3",isCancelled=false},
                    new Reservation{reservationID="ABC9",startTime= DateTime.Parse("2021-08-22 12:00:00"),hours=1,endTime= DateTime.Parse("2021-08-22 13:00:00"),parkingSpotID="A1",isCancelled=false},
                    new Reservation{reservationID="ABC10",startTime= DateTime.Parse("2021-07-22 14:00:00"),hours=1,endTime= DateTime.Parse("2021-07-22 15:00:00"),parkingSpotID="A1",isCancelled=false},
                    new Reservation{reservationID="ABC11",startTime= DateTime.Parse("2021-06-22 18:00:00"),hours=1,endTime= DateTime.Parse("2021-06-22 19:00:00"),parkingSpotID="I1",isCancelled=false},
                    new Reservation{reservationID="ABC30",startTime= DateTime.Parse("2021-02-01 01:00:00"),hours=22,endTime= DateTime.Parse("2021-02-01 23:00:00"),parkingSpotID="I5",isCancelled=false}

            };
            foreach (Reservation r in reservations)
            {
                context.Reservations.Add(r);
            }
            context.SaveChanges();

        }

    }
}
