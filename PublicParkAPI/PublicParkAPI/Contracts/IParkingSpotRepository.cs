using Microsoft.AspNetCore.Mvc;
using PublicParkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicParkAPI.Contracts
{
    public interface IParkingSpotRepository : IRepositoryBase<ParkingSpot>
    {
        IEnumerable<ParkingSpot> GetParkingSpots();
        ParkingSpot GetParkingSpot(string id);
        IEnumerable<ParkingSpot> GetParkingFreeSpots();
        IEnumerable<ParkingSpot> GetParkingSpecificFreeSpots(DateTime entryHour, DateTime leaveHour);
        IEnumerable<ParkingSpot>GetParkingPriceFreeSpots(decimal price);
        bool PutParkingSpot(ParkingSpot parkingSpot);
        bool PostParkingSpot(ParkingSpot parkingSpot);
        bool DeleteParkingSpot(string id);
        bool ParkingSpotExists(string id);
    }
}
