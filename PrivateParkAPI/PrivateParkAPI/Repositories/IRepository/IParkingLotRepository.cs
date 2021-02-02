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

        Task<ActionResult<ParkingLot>> PutParkingLot(int id, ParkingLot parkingLot);

        Task<ActionResult<ParkingLot>> PostParkingLot(ParkingLot parkingLot);

        Task<ActionResult<ParkingLot>> DeleteParkingLot(int id);
    }
}
