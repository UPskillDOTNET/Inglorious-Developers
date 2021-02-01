using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicParkAPI.Contracts;
using PublicParkAPI.Data;
using PublicParkAPI.Models;
using PublicParkAPI.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<ActionResult<ParkingLot>> PutParkingLot(int id, ParkingLot parkingLot)
        {
            await UpdateAsync(parkingLot);
            return parkingLot;
        }

        public async Task<ActionResult<ParkingLot>> PostParkingLot(ParkingLot parkingLot)
        {
            await AddAsync(parkingLot);
            return parkingLot;
        }

        public async Task<ActionResult<ParkingLot>> DeleteParkingLot(int id)
        {
            var parkingLot = GetAll().FirstOrDefault(l => l.parkingLotID == id);
            await DeleteAsync(parkingLot);
            return parkingLot;
        }


    }
}
