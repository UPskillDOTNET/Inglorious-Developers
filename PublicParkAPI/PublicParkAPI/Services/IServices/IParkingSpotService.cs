using Microsoft.AspNetCore.Mvc;
using PublicParkAPI.DTO;
using PublicParkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicParkAPI.Services
{
    public interface IParkingSpotService
    {
        Task<IEnumerable<ParkingSpotDTO>> GetParkingSpots();
        Task<ParkingSpotDTO> GetParkingSpot(string id);
        Task<IEnumerable<ParkingSpotDTO>> GetFreeParkingSpots();
        Task<ActionResult<ParkingSpotDTO>> PutParkingSpot(string id, ParkingSpotDTO parkingSpotDTO);
        Task<ActionResult<ParkingSpotDTO>> PostParkingSpot(ParkingSpotDTO parkingSpotDTO);
        Task<ActionResult<ParkingSpotDTO>> DeleteParkingSpot(string id);
        Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetParkingSpecificFreeSpots(DateTime entryHour, DateTime leaveHour);
        Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetParkingPriceFreeSpots(decimal price);
        Task<ParkingSpot> GetSpecificParkingSpot(ReservationDTO reservationDTO);

    }
}
