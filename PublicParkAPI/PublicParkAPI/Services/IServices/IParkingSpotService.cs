using PublicParkAPI.DTO;
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
    }
}
