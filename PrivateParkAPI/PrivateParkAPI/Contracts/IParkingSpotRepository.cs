using Microsoft.AspNetCore.Mvc;
using PrivateParkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateParkAPI.Contracts {
    public interface IParkingSpotRepository : IRepositoryBase<ParkingSpot> {
        IEnumerable<ParkingSpot> GetParkingSpots();
        ParkingSpot GetParkingSpot(string id);
        IEnumerable<ParkingSpot> GetParkingFreeSpots();
        IEnumerable<ParkingSpot> GetParkingSpecificFreeSpotsAsync(DateTime entryHour, DateTime leaveHour);
        IEnumerable<ParkingSpot> GetParkingPriceFreeSpotsAsync(decimal price);
        bool PutParkingSpot(ParkingSpot parkingSpot);
        bool PostParkingSpot(ParkingSpot parkingSpot);
        bool DeleteParkingSpot(string id);
        bool ParkingSpotExists(string id);
    }
}