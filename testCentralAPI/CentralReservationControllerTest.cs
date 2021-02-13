using AutoMapper;
using CentralAPI.Controllers;
using CentralAPI.Data;
using CentralAPI.DTO;
using CentralAPI.Models;
using CentralAPI.Repositories.Repository;
using CentralAPI.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace testCentralAPI {
    public class CentralReservationControllerTest {
    }
    
    
    public static class CentralAPI_CentralReservationContext
    {
        private static CentralAPIContext centralReservationContext;

        public static CentralAPIContext GetCentralAPIContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<CentralAPIContext>()
                            .UseInMemoryDatabase(databaseName: dbName)
                            .Options;

            centralReservationContext = new CentralAPIContext(options);
            Seed();
            return centralReservationContext;
        }

        private static void Seed()
        {
            centralReservationContext.CentralReservations.Add(new CentralReservation { reservationID = "ABC1", isCancelled = false, startTime = DateTime.Parse("2021-05-22 07:00:00"), endTime = DateTime.Parse("2021-05-22 08:00:00"), finalPrice = 10, parkingLotID = 1, userID = "1" });
            centralReservationContext.CentralReservations.Add(new CentralReservation { reservationID = "ABC2", isCancelled = true, startTime = DateTime.Parse("2021-01-31 07:00:00"), endTime = DateTime.Parse("2021-08-22 09:00:00"), finalPrice = 10, parkingLotID = 2, userID = "2" });
            centralReservationContext.CentralReservations.Add(new CentralReservation { reservationID = "ABC3", isCancelled = false, startTime = DateTime.Parse("2021-09-22 07:00:00"), endTime = DateTime.Parse("2021-09-22 19:00:00"), finalPrice = 10, parkingLotID = 1, userID = "3" });
            centralReservationContext.CentralReservations.Add(new CentralReservation { reservationID = "ABC4", isCancelled = false, startTime = DateTime.Parse("2021-10-22 07:00:00"), endTime = DateTime.Parse("2021-10-22 10:00:00"), finalPrice = 10, parkingLotID = 2, userID = "4" });
            centralReservationContext.CentralReservations.Add(new CentralReservation { reservationID = "ABC5", isCancelled = false, startTime = DateTime.Parse("2021-09-22 07:00:00"), endTime = DateTime.Parse("2021-09-22 08:00:00"), finalPrice = 10, parkingLotID = 2, userID = "5" });
            centralReservationContext.CentralReservations.Add(new CentralReservation { reservationID = "ABC6", isCancelled = false, startTime = DateTime.Parse("2021-08-22 12:00:00"), endTime = DateTime.Parse("2021-08-22 13:00:00"), finalPrice = 10, parkingLotID = 1, userID = "6" });
            centralReservationContext.CentralReservations.Add(new CentralReservation { reservationID = "ABC7", isCancelled = true, startTime = DateTime.Parse("2021-07-22 14:00:00"), endTime = DateTime.Parse("2021-07-22 15:00:00"), finalPrice = 10, parkingLotID = 2, userID = "7" });
            centralReservationContext.CentralReservations.Add(new CentralReservation { reservationID = "ABC8", isCancelled = false, startTime = DateTime.Parse("2021-06-22 18:00:00"), endTime = DateTime.Parse("2021-06-22 19:00:00"), finalPrice = 10, parkingLotID = 1, userID = "8" });

            centralReservationContext.SaveChanges();
        }
    }
}
