﻿using PublicParkAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PublicParkAPI.Contracts
{
    public interface IParkingSpotRepository : IBaseRepository<ParkingSpot>
    {
        Task<IEnumerable<ParkingSpot>> GetAllParkingSpots();
        Task<IEnumerable<ParkingSpot>> GetParkingSpotbyPrice(decimal priceHour);
        Task<ParkingSpot> GetParkingSpot(string id);
        Task<ParkingSpot> FindParkingSpot(string id);
        Task<bool> FindParkingSpotAny(string id);
        Task<ParkingSpot> PutParkingSpot(ParkingSpot parkingSpot);
        Task<ParkingSpot> PostParkingSpot(ParkingSpot parkingSpot);
        Task<ParkingSpot> DeleteParkingSpot(string id);
    }
}
