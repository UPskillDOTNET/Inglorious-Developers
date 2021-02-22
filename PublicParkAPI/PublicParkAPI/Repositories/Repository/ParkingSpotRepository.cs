using Microsoft.EntityFrameworkCore;
using PublicParkAPI.Contracts;
using PublicParkAPI.Data;
using PublicParkAPI.Models;
using PublicParkAPI.Repositories.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicParkAPI.Repositories
{
    public class ParkingSpotRepository : BaseRepository<ParkingSpot>, IParkingSpotRepository
    {
        public ParkingSpotRepository(PublicParkContext publicParkContext) : base(publicParkContext)
        {
        }

        public async Task<IEnumerable<ParkingSpot>> GetParkingSpotbyPrice(decimal priceHour)
        {
            return await GetAll().Where(p => p.priceHour <= priceHour).ToListAsync();
        }

        public async Task<IEnumerable<ParkingSpot>> GetAllParkingSpots()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<ParkingSpot> GetParkingSpot(string id)
        {
            return await GetAll().FirstOrDefaultAsync(s => s.parkingSpotID == id);

        }

        public async Task<ParkingSpot> FindParkingSpot(string id)
        {
            return await Find(id);
        }

        public async Task<bool> FindParkingSpotAny(string id)
        {
            return await GetAll().Where(p => p.parkingSpotID == id).AnyAsync();
        }

        public async Task<ParkingSpot> PutParkingSpot(ParkingSpot parkingSpot)
        {
            parkingSpot = await UpdateAsync(parkingSpot);

            return parkingSpot;
        }

        public async Task<ParkingSpot> PostParkingSpot(ParkingSpot parkingSpot)
        {
            parkingSpot = await AddAsync(parkingSpot);

            return parkingSpot;
        }


        public async Task<ParkingSpot> DeleteParkingSpot(string id)
        {
            var parkingSpot = GetAll().FirstOrDefault(s => s.parkingSpotID == id);

            await DeleteAsync(parkingSpot);

            return parkingSpot;
        }
    }
}

