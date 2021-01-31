using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
        
        Task<HttpStatusCode> PutParkingSpot(string id, ParkingSpotDTO parkingSpotDTO);


    }
}
