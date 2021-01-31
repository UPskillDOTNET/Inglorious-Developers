using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrivateParkAPI.Models;

namespace PrivateParkAPI.Repositories.IRepository
{
    public interface IParkingSpotRepository: IBaseRepository<ParkingSpot>
    {
        Task<IEnumerable<ParkingSpot>> GetnotPrivateParkingSpots();
        //Task<List<ParkingSpotDTO>> GetAllParkingSpots();
        //Task<List<ParkingSpotDTO>> GetParkingFreeSpots();
        //Task<List<ParkingSpotDTO>> GetParkingSpecificFreeSpots();
        //Task<List<ParkingSpotDTO>> GetParkingPriceFreeSpots();
        //Task<List<ParkingSpotDTO>> GetParkingCoveredFreeSpots();
        //Task <ParkingSpotDTO> GetParkingSpot();
        //Task<ParkingSpotDTO> PutParkingSpot();
        //Task<ParkingSpotDTO> PostParkingSpot();
        //Task<ParkingSpotDTO> DeleteParkingSpot();
        //bool ParkingSpotExists();
    }
}
