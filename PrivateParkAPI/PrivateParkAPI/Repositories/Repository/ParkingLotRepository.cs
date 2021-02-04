using Microsoft.EntityFrameworkCore;
using PrivateParkAPI.Data;
using PrivateParkAPI.Models;
using PrivateParkAPI.Repositories.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrivateParkAPI.Repositories.Repository
{
    public class ParkingLotRepository : BaseRepository<ParkingLot>, IParkingLotRepository
    {
        public ParkingLotRepository(PrivateParkContext privateParkContext) : base(privateParkContext)
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