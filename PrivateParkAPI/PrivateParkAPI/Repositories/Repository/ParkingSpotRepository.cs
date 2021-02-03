using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrivateParkAPI.Repositories.IRepository;
using PrivateParkAPI.Data;
using PrivateParkAPI.Models;
using Microsoft.EntityFrameworkCore;
using PrivateParkAPI.DTO;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace PrivateParkAPI.Repositories.Repository
{
    public class ParkingSpotRepository : BaseRepository<ParkingSpot>, IParkingSpotRepository
    {
        public ParkingSpotRepository(PrivateParkContext privateParkContext) : base(privateParkContext)
        {
        }
        public async Task<IEnumerable<ParkingSpot>> GetnotPrivateParkingSpots()
        {

            return await GetAll().Where(p => p.isPrivate == false).Include(p => p.ParkingLot).ToListAsync();
        }

        public async Task<IEnumerable<ParkingSpot>> GetParkingSpotbyPrice(decimal priceHour)
        {
            return await GetAll().Where(p => p.priceHour <= priceHour).Where(p => p.isPrivate == false).Include(p => p.ParkingLot).ToListAsync();
        }

        public async Task<IEnumerable<ParkingSpot>> GetAllParkingSpots()
        {
            return await GetAll().Include(p => p.ParkingLot).ToListAsync();
        }

        public async Task<ParkingSpot> GetParkingSpot(string id)
        {
            return await GetAll().Include(p => p.ParkingLot).FirstOrDefaultAsync(s => s.parkingSpotID == id);
        }

        public async Task<ParkingSpot> FindParkingSpot(string id)
        {
            return await Find(id);
        }

        public async Task<bool> FindParkingSpotAny(string id)
        {
            return await GetAll().Where(p => p.parkingSpotID == id).AnyAsync();
        }

        public async Task<ParkingSpot> PutParkingSpot(string id, ParkingSpot parkingSpot)
        {
            await UpdateAsync(parkingSpot);

            return parkingSpot;
        }

        public async Task<ParkingSpot> PostParkingSpot(ParkingSpot parkingSpot)
        {

            await AddAsync(parkingSpot);

            return parkingSpot;
        }

        public async Task<ParkingSpot> DeleteParkingSpot(string id)
        {
            var parkingSpot = GetAll().First(s => s.parkingSpotID == id);

            await DeleteAsync(parkingSpot);

            return parkingSpot;
        }


    }
}
