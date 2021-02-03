using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrivateParkAPI.DTO;
using PrivateParkAPI.Models;
using Microsoft.AspNetCore.Mvc;

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
