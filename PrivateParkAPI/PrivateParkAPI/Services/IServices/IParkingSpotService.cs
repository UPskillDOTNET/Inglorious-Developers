using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PrivateParkAPI.DTO;
using PrivateParkAPI.Models;

namespace PrivateParkAPI.Services.IServices
{
    public interface IParkingSpotService
    {
        Task<IEnumerable<ParkingSpotDTO>> GetAllnotPrivate();
        Task<IEnumerable<ParkingSpotDTO>> GetAllParkingSpots();
        Task<IEnumerable<ParkingSpotDTO>> GetFreeParkingSpots();
        Task<ParkingSpotDTO> GetParkingSpot(string id);
        Task <ActionResult<ParkingSpotDTO>> PutParkingSpot(string id, ParkingSpotDTO parkingSpotDTO);
        Task<ActionResult<ParkingSpotDTO>> PostParkingSpot( ParkingSpotDTO parkingSpotDTO);
        Task<ActionResult<ParkingSpotDTO>> DeleteParkingSpot(string id);
       



    }
}
