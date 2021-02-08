using CentralAPI.Data;
using CentralAPI.Models;
using CentralAPI.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Repositories.Repository
{
    public class ParkingLotRepository : BaseRepository<ParkingLot>, IParkingLotRepository
    {
        public ParkingLotRepository(CentralParkContext centralParkContext) : base(centralParkContext)
        {
        }
        public async Task<IEnumerable<ParkingLot>> GetParkingLots()
        {

            return await GetAll().ToListAsync();
        }
        public async Task<ParkingLot> GetParkingLot(int id)
        {
            return await GetAll().FirstOrDefaultAsync(l => l.parkingLotID == id);
        }

        public async Task<ParkingLot> PutParkingLot(int id, ParkingLot parkingLot)
        {
            await UpdateAsync(parkingLot);
            return parkingLot;
        }

        public async Task<ParkingLot> PostParkingLot(ParkingLot parkingLot)
        {
            await AddAsync(parkingLot);
            return parkingLot;
        }
    }
}
