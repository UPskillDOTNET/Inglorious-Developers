using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrivateParkAPI.Repositories.IRepository;
using PrivateParkAPI.Data;
using PrivateParkAPI.Models;
using Microsoft.EntityFrameworkCore;
using PrivateParkAPI.DTO;
using Microsoft.AspNetCore.Mvc;

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