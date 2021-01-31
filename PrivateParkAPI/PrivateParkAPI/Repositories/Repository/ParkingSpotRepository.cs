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
            
            return await GetAll().Where(p=>p.isPrivate == false).Include(p=>p.ParkingLot).ToListAsync();
        }


        public async Task<IEnumerable<ParkingSpot>> GetAllParkingSpots()
        {
            return await GetAll().Include(p => p.ParkingLot).ToListAsync();
        }

        // GET: api/ParkingSpots/5
        
        public async Task<ParkingSpot> GetParkingSpot(string id)
        {
            return await GetAll().Include(p => p.ParkingLot).FirstOrDefaultAsync(s => s.parkingSpotID == id);
        }

        ////Get: Available Spots
        //[Route("~/api/parkingspots/freeSpots")]
        //public async Task<ActionResult<IEnumerable<ParkingSpot>>> GetParkingFreeSpots()
        //{

        //    var reservation = await _context.Reservations.Where(r => r.startTime <= DateTime.Now && r.endTime >= DateTime.Now).Include(s => s.ParkingSpot).ThenInclude(s => s.ParkingLot).ToListAsync();
        //    var parkingSpots = await _context.ParkingSpots.Include(p => p.ParkingLot).ToListAsync();

        //    foreach (var r in reservation)
        //    {
        //        parkingSpots.Remove(r.ParkingSpot);
        //    }
        //    return parkingSpots;
        //}

        ////Get: Available Specific Spots
        //[Route("~/api/parkingspots/freeSpots/{entryHour}/{leaveHour}")]
        //public async Task<ActionResult<IEnumerable<ParkingSpot>>> GetParkingSpecificFreeSpots(DateTime entryHour, DateTime leaveHour)
        //{
        //    if (entryHour > leaveHour)
        //    {
        //        return BadRequest("Can't leave before you enter");
        //    }
        //    var reservation = await _context.Reservations.Where(r => r.startTime <= leaveHour && r.endTime >= entryHour).Include(s => s.ParkingSpot).ThenInclude(s => s.ParkingLot).ToListAsync();
        //    var parkingSpots = await _context.ParkingSpots.Include(p => p.ParkingLot).ToListAsync();


        //    foreach (var r in reservation)
        //    {
        //        parkingSpots.Remove(r.ParkingSpot);
        //    }
        //    return parkingSpots;
        //}

        ////Get: Available Parking Spots by price
        //[Route("~/api/parkingspots/freeSpots/{price}")]

        //public async Task<ActionResult<IEnumerable<ParkingSpot>>> GetParkingPriceFreeSpots(Decimal price)
        //{
        //    if (price <= 0)
        //    {
        //        return BadRequest("We dont sell stuff for free");
        //    }
        //    var reservation = await _context.Reservations.Where(r => r.startTime <= DateTime.Now && r.endTime >= DateTime.Now).Include(s => s.ParkingSpot).ThenInclude(s => s.ParkingLot).ToListAsync();
        //    var parkingSpots = await _context.ParkingSpots.Where(p => p.priceHour <= price).Include(p => p.ParkingLot).ToListAsync();


        //    foreach (var r in reservation)
        //    {
        //        parkingSpots.Remove(r.ParkingSpot);
        //    }
        //    return parkingSpots;
        //}

        ////Get: Available Covered Parking Spots 
        //[Route("~/api/parkingspots/freeSpots/isCovered")]

        //public async Task<ActionResult<IEnumerable<ParkingSpot>>> GetParkingCoveredFreeSpots(Boolean isCovered)
        //{

        //    var reservation = await _context.Reservations.Where(r => r.startTime <= DateTime.Now && r.endTime >= DateTime.Now).Include(s => s.ParkingSpot).ThenInclude(s => s.ParkingLot).ToListAsync();
        //    var parkingSpots = await _context.ParkingSpots.Where(c => c.isCovered == true).Include(p => p.ParkingLot).ToListAsync();


        //    foreach (var r in reservation)
        //    {
        //        parkingSpots.Remove(r.ParkingSpot);
        //    }
        //    return parkingSpots;
        //}

        // PUT: api/ParkingSpots/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
 
        public async Task<ActionResult<ParkingSpot>>PutParkingSpot(string id, ParkingSpot parkingSpot)
        {
             await UpdateAsync(parkingSpot);
            
            return  parkingSpot;
        }

        //// POST: api/ParkingSpots
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<ParkingSpot>> PostParkingSpot(ParkingSpot parkingSpot)
        //{
        //    _context.ParkingSpots.Add(parkingSpot);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (ParkingSpotExists(parkingSpot.parkingSpotID))
        //        {
        //            return Conflict("Parking Spot already Exist");
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    return CreatedAtAction("GetParkingSpot", new { id = parkingSpot.parkingSpotID }, parkingSpot);
        //}

        //// DELETE: api/ParkingSpots/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteParkingSpot(string id)
        //{
        //    var parkingSpot = await _context.ParkingSpots.FindAsync(id);
        //    if (parkingSpot == null)
        //    {
        //        return NotFound("Can't Delete a Parking Spot that does not Exist");
        //    }

        //    _context.ParkingSpots.Remove(parkingSpot);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool ParkingSpotExists(string id)
        //{
        //    return GetAll().FirstOrDefaultAsync(e => e.parkingSpotID == id);
        //}
    }
}
