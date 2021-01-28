using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrivateParkAPI.Data;
using PrivateParkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateParkAPI.Controllers
{
    [Authorize]
    [Route("api/parkingspots")]
    [ApiController]
    public class ParkingSpotsController : ControllerBase
    {
        private readonly PrivateParkContext _context;

        public ParkingSpotsController(PrivateParkContext context)
        {
            _context = context;
        }

        // GET: api/ParkingSpots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingSpot>>> GetnotPrivateParkingSpots()
        {
            return await _context.ParkingSpots.Include(p => p.ParkingLot).Where(p => p.isPrivate == false).ToListAsync();
        }

        [Route("~/api/parkingspots/all")]
        public async Task<ActionResult<IEnumerable<ParkingSpot>>> GetAllParkingSpots()
        {
            return await _context.ParkingSpots.Include(p => p.ParkingLot).ToListAsync();
        }

        // GET: api/ParkingSpots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingSpot>> GetParkingSpot(string id)
        {
            var parkingSpot = await _context.ParkingSpots.Include(p => p.ParkingLot).FirstOrDefaultAsync(s => s.parkingSpotID == id);

            if (parkingSpot == null)
            {
                return NotFound();
            }

            return parkingSpot;
        }

        //Get: Available Spots
        [Route("~/api/parkingspots/freeSpots")]
        public async Task<ActionResult<IEnumerable<ParkingSpot>>> GetParkingFreeSpots()
        {

            var reservation = await _context.Reservations.Where(r => r.startTime <= DateTime.Now && r.endTime >= DateTime.Now).Include(s => s.ParkingSpot).ThenInclude(s => s.ParkingLot).ToListAsync();
            var parkingSpots = await _context.ParkingSpots.Include(p => p.ParkingLot).ToListAsync();

            foreach (var r in reservation)
            {
                parkingSpots.Remove(r.ParkingSpot);
            }
            return parkingSpots;
        }

        //Get: Available Specific Spots
        [Route("~/api/parkingspots/freeSpots/{entryHour}/{leaveHour}")]
        public async Task<ActionResult<IEnumerable<ParkingSpot>>> GetParkingSpecificFreeSpots(DateTime entryHour, DateTime leaveHour)
        {
            if (entryHour > leaveHour)
            {
                return BadRequest();
            }
            var reservation = await _context.Reservations.Where(r => r.startTime <= leaveHour && r.endTime >= entryHour).Include(s => s.ParkingSpot).ThenInclude(s => s.ParkingLot).ToListAsync();
            var parkingSpots = await _context.ParkingSpots.Include(p => p.ParkingLot).ToListAsync();


            foreach (var r in reservation)
            {
                parkingSpots.Remove(r.ParkingSpot);
            }
            return parkingSpots;
        }

        //Get: Available Parking Spots by price
        [Route("~/api/parkingspots/freeSpots/{price}")]

        public async Task<ActionResult<IEnumerable<ParkingSpot>>> GetParkingPriceFreeSpots(Decimal price)
        {
            if (price < 0)
            {
                return BadRequest();
            }
            var reservation = await _context.Reservations.Where(r => r.startTime <= DateTime.Now && r.endTime >= DateTime.Now).Include(s => s.ParkingSpot).ThenInclude(s => s.ParkingLot).ToListAsync();
            var parkingSpots = await _context.ParkingSpots.Where(p => p.priceHour <= price).Include(p => p.ParkingLot).ToListAsync();


            foreach (var r in reservation)
            {
                parkingSpots.Remove(r.ParkingSpot);
            }
            return parkingSpots;
        }


        // PUT: api/ParkingSpots/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParkingSpot(string id, ParkingSpot parkingSpot)
        {
            if (id != parkingSpot.parkingSpotID)
            {
                return BadRequest();
            }

            _context.Entry(parkingSpot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkingSpotExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ParkingSpots
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ParkingSpot>> PostParkingSpot(ParkingSpot parkingSpot)
        {
            _context.ParkingSpots.Add(parkingSpot);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ParkingSpotExists(parkingSpot.parkingSpotID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return CreatedAtAction("GetParkingSpot", new { id = parkingSpot.parkingSpotID }, parkingSpot);
        }

        // DELETE: api/ParkingSpots/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParkingSpot(string id)
        {
            var parkingSpot = await _context.ParkingSpots.FindAsync(id);
            if (parkingSpot == null)
            {
                return NotFound();
            }

            _context.ParkingSpots.Remove(parkingSpot);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParkingSpotExists(string id)
        {
            return _context.ParkingSpots.Any(e => e.parkingSpotID == id);
        }
    }
}
