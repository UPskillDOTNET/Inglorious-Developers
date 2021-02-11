using Microsoft.EntityFrameworkCore;
using PublicParkAPI.Contracts;
using PublicParkAPI.Data;
using PublicParkAPI.Models;
using PublicParkAPI.Repositories.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PublicParkAPI.Repositories
{
    public class ParkingLotRepository : BaseRepository<ParkingLot>, IParkingLotRepository
    {
        public ParkingLotRepository(PublicParkContext publicParkContext) : base(publicParkContext)
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

        public async Task<ParkingLot> PutParkingLot(ParkingLot parkingLot)
        {
            parkingLot = await UpdateAsync(parkingLot);
            return parkingLot;
        }

        public async Task<ParkingLot> PostParkingLot(ParkingLot parkingLot)
        {
            parkingLot = await AddAsync(parkingLot);
            return parkingLot;
        }
    }
}
