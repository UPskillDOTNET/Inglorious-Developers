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

        public async Task<IEnumerable<ParkingSpot>> GetFreeParkingSpots()
        {
            return await GetAll().Include(p => p.ParkingLot).ToListAsync();
        }

        //public IEnumerable<ParkingSpot> GetParkingSpecificFreeSpots(DateTime entryHour, DateTime leaveHour)
        //{

        //    var reservation = await _context.Reservations.Where(r => r.startTime <= leaveHour && r.endTime >= entryHour).Include(s => s.ParkingSpot).ThenInclude(s => s.ParkingLot).ToListAsync();
        //    var parkingSpots = await _context.ParkingSpots.Include(p => p.ParkingLot).ToListAsync();

        //    foreach (var r in reservation)
        //    {
        //        parkingSpots.Remove(r.ParkingSpot);
        //    }
        //    return parkingSpots;
        //}

        //public IEnumerable<ParkingSpot> GetParkingPriceFreeSpots(decimal price)
        //{

        //    var reservation = await _context.Reservations.Where(r => r.startTime <= DateTime.Now && r.endTime >= DateTime.Now).Include(s => s.ParkingSpot).ThenInclude(s => s.ParkingLot).ToListAsync();
        //    var parkingSpots = await _context.ParkingSpots.Where(p => p.priceHour <= price).Include(p => p.ParkingLot).ToListAsync();

        //    foreach (var r in reservation)
        //    {
        //        parkingSpots.Remove(r.ParkingSpot);
        //    }
        //    return parkingSpots;
        //}

        //public bool ParkingSpotExists(string id)
        //{
        //    _context.ParkingSpots.AnyAsync(q => q.parkingSpotID.Equals(id));
        //    return true;
        //}

        //public bool PostParkingSpot(ParkingSpot parkingSpot)
        //{
        //    _context.ParkingSpots.Add(parkingSpot);
        //    _context.SaveChangesAsync();
        //    return true;
        //}

        //public bool PutParkingSpot(ParkingSpot parkingSpot)
        //{
        //    _context.Entry(parkingSpot).State = EntityState.Modified;
        //    _context.SaveChangesAsync();
        //    return true;
        //}

        //public bool DeleteParkingSpot(string id)
        //{
        //    var parkingSpot =  _context.ParkingSpots.Find(id);
        //    _context.ParkingSpots.Remove(parkingSpot);
        //    _context.SaveChangesAsync();
        //    return true;
        //}
    }
}

