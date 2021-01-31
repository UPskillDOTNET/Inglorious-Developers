using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrivateParkAPI.DTO;
using PrivateParkAPI.Models;

namespace PrivateParkAPI.Repositories.IRepository
{
    public interface IParkingLotRepository : IBaseRepository<ParkingLot>
    {
        Task<IEnumerable<ParkingLot>> GetParkingLots();
    }
}
