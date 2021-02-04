using PrivateParkAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrivateParkAPI.Repositories.IRepository
{
    public interface IParkingLotRepository : IBaseRepository<ParkingLot>
    {
        Task<IEnumerable<ParkingLot>> GetParkingLots();
        Task<ParkingLot> GetParkingLot(int id);
        Task<ParkingLot> PutParkingLot(int id, ParkingLot parkingLot);
        Task<ParkingLot> PostParkingLot(ParkingLot parkingLot);
    }
}
