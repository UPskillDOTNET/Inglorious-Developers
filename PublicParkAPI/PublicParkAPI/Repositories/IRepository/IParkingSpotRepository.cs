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
        Task<IEnumerable<ParkingSpot>> GetCheaperParkingSpots(decimal price);
        Task<ActionResult<ParkingSpot>> PutParkingSpot(string id, ParkingSpot parkingSpot);
        Task<ActionResult<ParkingSpot>> PostParkingSpot(ParkingSpot parkingSpot);
        Task<ActionResult<ParkingSpot>> DeleteParkingSpot(string id);
        Task<ParkingSpot> GetSpecificParkingSpot(ReservationDTO reservationDTO);
        //bool ParkingSpotExists(string id);
    }
}
