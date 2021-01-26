using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicParkAPI.Data;
using PublicParkAPI.Models;

namespace PublicParkAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingSpotsController : ControllerBase
    {
        private readonly PublicParkContext _context;

        public ParkingSpotsController(PublicParkContext context)
        {
            _context = context;
        }

        // GET: api/ParkingSpots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingSpot>>> GetParkingSpots()
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
