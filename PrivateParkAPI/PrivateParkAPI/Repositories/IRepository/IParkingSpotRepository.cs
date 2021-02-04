using PrivateParkAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrivateParkAPI.Repositories.IRepository
{
    public interface IParkingSpotRepository : IBaseRepository<ParkingSpot>
    {
        Task<IEnumerable<ParkingSpot>> GetnotPrivateParkingSpots();
        Task<IEnumerable<ParkingSpot>> GetAllParkingSpots();
        Task<IEnumerable<ParkingSpot>> GetParkingSpotbyPrice(decimal priceHour);
        Task<ParkingSpot> GetParkingSpot(string ID);
        Task<ParkingSpot> FindParkingSpot(string id);
        Task<bool> FindParkingSpotAny(string id);
        Task<ParkingSpot> PutParkingSpot(string id, ParkingSpot parkingSpot);
        Task<ParkingSpot> PostParkingSpot(ParkingSpot parkingSpot);
        Task<ParkingSpot> DeleteParkingSpot(string id);

    }
}
