using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicParkAPI.Contracts;
using PublicParkAPI.Data;
using PublicParkAPI.DTO;
using PublicParkAPI.Models;
using PublicParkAPI.Repositories.Repository;
using System;
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

        public async Task<IEnumerable<ParkingSpot>> GetParkingSpots()
        {
            return await GetAll().Include(p => p.ParkingLot).ToListAsync();
        }


        public async Task<ParkingSpot> GetParkingSpot(string id)
        {
            return await GetAll().Include(p => p.ParkingLot).FirstOrDefaultAsync(s => s.parkingSpotID == id);

        }

        public async Task<ParkingSpot> GetSpecificParkingSpot(ReservationDTO reservationDTO)
        {
            return await GetAll().Include(p => p.ParkingLot).FirstOrDefaultAsync(s => s.parkingSpotID == reservationDTO.parkingSpotID);

        }
        public async Task<IEnumerable<ParkingSpot>> GetCheaperParkingSpots(decimal price)
        {
            return await GetAll().Where(p => p.priceHour <= price).Include(p => p.ParkingLot).ToListAsync();
        }

        public async Task<ActionResult<ParkingSpot>> PutParkingSpot(string id, ParkingSpot parkingSpot)
        {
            await UpdateAsync(parkingSpot);
            return parkingSpot;
        }

        public async Task<ActionResult<ParkingSpot>> PostParkingSpot(ParkingSpot parkingSpot)
        {
            await AddAsync(parkingSpot);
            return parkingSpot;
        }

        public async Task<ActionResult<ParkingSpot>> DeleteParkingSpot(string id)
        {
            var parkingSpot = GetAll().FirstOrDefault(s => s.parkingSpotID == id);
            await DeleteAsync(parkingSpot);
            return parkingSpot;
        }
    }
}

