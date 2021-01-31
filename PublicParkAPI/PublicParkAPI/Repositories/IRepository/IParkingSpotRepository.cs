using Microsoft.AspNetCore.Mvc;
using PublicParkAPI.DTO;
using PublicParkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicParkAPI.Contracts
{
    public interface IParkingSpotRepository : IBaseRepository<ParkingSpot>
    {
        Task<IEnumerable<ParkingSpot>> GetParkingSpots();
        Task<ParkingSpot> GetParkingSpot(string id);
        //Task<IEnumerable<ParkingSpot>> GetParkingSpecificFreeSpots(DateTime entryHour, DateTime leaveHour);
        //Task<IEnumerable<ParkingSpotDTO>> GetParkingPriceFreeSpots(decimal price);
        Task<ActionResult<ParkingSpot>> PutParkingSpot(string id, ParkingSpot parkingSpot);
        //Task<ParkingSpotDTO> PostParkingSpot(ParkingSpot parkingSpot);
        //Task<ParkingSpotDTO> DeleteParkingSpot(string id);
        //bool ParkingSpotExists(string id);
    }
}
