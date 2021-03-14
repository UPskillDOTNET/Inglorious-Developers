using PrivateParkAPI.Models;
using System;
using System.Linq;

namespace PrivateParkAPI.Data {
    public class DBInitializer {
        public static void Initialize(PrivateParkContext context) {
            context.Database.EnsureCreated();

            if (context.ParkingSpots.Any()) {
                return;   // DB has been seeded
            }

            var parkingSpots = new ParkingSpot[]
            {
                    new ParkingSpot{parkingSpotID="A1",priceHour=0.250m,isPrivate=true, floor=1},
                    new ParkingSpot{parkingSpotID="A2",priceHour=0.250m,isPrivate=true, floor=1},
                    new ParkingSpot{parkingSpotID="A3",priceHour=0.25m,isPrivate=false, floor=1},
                    new ParkingSpot{parkingSpotID="A4",priceHour=0.250m,isPrivate=true, floor=1},
                    new ParkingSpot{parkingSpotID="A5",priceHour=0.250m,isPrivate=true, floor=1},
                    new ParkingSpot{parkingSpotID="E1",priceHour=0.5m,isPrivate=false, floor=2},
                    new ParkingSpot{parkingSpotID="E2",priceHour=0.5m,isPrivate=false, floor=2},
                    new ParkingSpot{parkingSpotID="E3",priceHour=0.5m,isPrivate=false, floor=2},
                    new ParkingSpot{parkingSpotID="E4",priceHour=0.5m,isPrivate=false, floor=2},
                    new ParkingSpot{parkingSpotID="E5",priceHour=0.5m,isPrivate=false, floor=2},
                    new ParkingSpot{parkingSpotID="I1",priceHour=0.9m,isPrivate=true, floor=1},
                    new ParkingSpot{parkingSpotID="I2",priceHour=0.9m,isPrivate=true, floor=1},
                    new ParkingSpot{parkingSpotID="I3",priceHour=0.9m,isPrivate=true, floor=1},
                    new ParkingSpot{parkingSpotID="I4",priceHour=0.9m,isPrivate=true, floor=1},
                    new ParkingSpot{parkingSpotID="I5",priceHour=0.9m,isPrivate=true, floor=1},
                    new ParkingSpot{parkingSpotID="O1",priceHour=1.00m,isPrivate=false, floor=3},
                    new ParkingSpot{parkingSpotID="O2",priceHour=0.10m,isPrivate=false, floor=3},
                    new ParkingSpot{parkingSpotID="O3",priceHour=1.00m,isPrivate=false, floor=3},
                    new ParkingSpot{parkingSpotID="O4",priceHour=1.00m,isPrivate=false, floor=3},
                    new ParkingSpot{parkingSpotID="O5",priceHour=1.00m,isPrivate=false, floor=3},
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
                    new Reservation{reservationID="ABC9",isCancelled=false,startTime= DateTime.Parse("2021-02-17 10:00:00"),hours=1,endTime= DateTime.Parse("2021-02-19 19:00:00"), parkingSpotID="A1"}
            };
            foreach (Reservation r in reservations) {
                context.Reservations.Add(r);
            }
            context.SaveChanges();

        }

    }
}