using CentralAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Repositories.IRepository
{
    public interface IParkingLotRepository : IBaseRepository<ParkingLot>
    {
        Task<IEnumerable<ParkingLot>> GetParkingLots();
        Task<ParkingLot> GetParkingLot(int id);
        Task<ParkingLot> GetParkingLotsByManagerId(string managerID);
        Task<ParkingLot> PutParkingLot(int id, ParkingLot parkingLot);
        Task<ParkingLot> PostParkingLot(ParkingLot parkingLot);
    }
}
