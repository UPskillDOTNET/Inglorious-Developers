using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrivateParkAPI.Repositories.IRepository;
using PrivateParkAPI.Data;
using PrivateParkAPI.Models;
using Microsoft.EntityFrameworkCore;
using PrivateParkAPI.DTO;

namespace PrivateParkAPI.Repositories.Repository
{
    public class ParkingLotRepository : BaseRepository<ParkingLot>, IParkingLotRepository
    {
        public ParkingLotRepository(PrivateParkContext privateParkContext) : base(privateParkContext)
        {
        }
        public async Task<IEnumerable<ParkingLot>> GetParkingLots()
        {

            return await GetParkingLots();
        }
    }

    //[Authorize]
    //[Route("api/parkinglots")]
    //[ApiController]
    //public class ParkingLotsController : ControllerBase
    //{
    //    private readonly PrivateParkContext _context;

    //    public ParkingLotsController(PrivateParkContext context)
    //    {
    //        _context = context;
    //    }

    //    // GET: api/ParkingLots
    //    [HttpGet]
    //    public async Task<ActionResult<IEnumerable<ParkingLot>>> GetParkingLots()
    //    {
    //        return await _context.ParkingLots.ToListAsync();
    //    }

    //    // GET: api/ParkingLots/5
    //    [HttpGet("{id}")]
    //    public async Task<ActionResult<ParkingLot>> GetParkingLot(int id)
    //    {
    //        var parkingLot = await _context.ParkingLots.FindAsync(id);

    //        if (parkingLot == null)
    //        {
    //            return NotFound("Parking Lot does not Exist");
    //        }

    //        return parkingLot;
    //    }

    //    // PUT: api/ParkingLots/5
    //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //    [HttpPut("{id}")]
    //    public async Task<IActionResult> PutParkingLot(int id, ParkingLot parkingLot)
    //    {
    //        if (!ModelState.IsValid || !id.Equals(parkingLot.parkingLotID))
    //        {
    //            return BadRequest();
    //        }

    //        _context.Entry(parkingLot).State = EntityState.Modified;

    //        try
    //        {
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!ParkingLotExists(id))
    //            {
    //                return NotFound("Can't Update a Parking Lot that does not Exist");
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }

    //        return NoContent();
    //    }

    //    // POST: api/ParkingLots
    //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //    [HttpPost]
    //    public async Task<ActionResult<ParkingLot>> PostParkingLot(ParkingLot parkingLot)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }

    //        _context.ParkingLots.Add(parkingLot);
    //        await _context.SaveChangesAsync();

    //        return CreatedAtAction("GetParkingLot", new { id = parkingLot.parkingLotID }, parkingLot);
    //    }

    //    // DELETE: api/ParkingLots/5
    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> DeleteParkingLot(int id)
    //    {
    //        var parkingLot = await _context.ParkingLots.FindAsync(id);
    //        if (parkingLot == null)
    //        {
    //            return NotFound("Can't Delete a Parking Lot that does not Exist");
    //        }

    //        _context.ParkingLots.Remove(parkingLot);
    //        await _context.SaveChangesAsync();

    //        return NoContent();
    //    }

    //    private bool ParkingLotExists(int id)
    //    {
    //        return _context.ParkingLots.Any(e => e.parkingLotID == id);
    //    }
    //}
}