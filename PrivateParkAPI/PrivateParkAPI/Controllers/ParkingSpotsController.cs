﻿//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using PrivateParkAPI.Data;
//using PrivateParkAPI.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace PrivateParkAPI.Controllers
//{
//    [Authorize]
//    [Route("api/parkingspots")]
//    [ApiController]
//    public class ParkingSpotsController : ControllerBase
//    {
//        private readonly PrivateParkContext _context;

//        public ParkingSpotsController(PrivateParkContext context)
//        {
//            _context = context;
//        }

//        // GET: api/ParkingSpots
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<ParkingSpot>>> GetnotPrivateParkingSpots()
//        {
//            return await _context.ParkingSpots.Include(p => p.ParkingLot).Where(p => p.isPrivate == false).ToListAsync();
//        }

//        [Route("~/api/parkingspots/all")]
//        public async Task<ActionResult<IEnumerable<ParkingSpot>>> GetAllParkingSpots()
//        {
//            return await _context.ParkingSpots.Include(p => p.ParkingLot).ToListAsync();
//        }

//        // GET: api/ParkingSpots/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<ParkingSpot>> GetParkingSpot(string id)
//        {
//            var parkingSpot = await _context.ParkingSpots.Include(p => p.ParkingLot).FirstOrDefaultAsync(s => s.parkingSpotID == id);

//            if (parkingSpot == null)
//            {
//                return NotFound("Parking Spot does not Exist");
//            }

//            return parkingSpot;
//        }

//        //Get: Available Spots
//        [Route("~/api/parkingspots/freeSpots")]
//        public async Task<ActionResult<IEnumerable<ParkingSpot>>> GetParkingFreeSpots()
//        {

//            var reservation = await _context.Reservations.Where(r => r.startTime <= DateTime.Now && r.endTime >= DateTime.Now).Include(s => s.ParkingSpot).ThenInclude(s => s.ParkingLot).ToListAsync();
//            var parkingSpots = await _context.ParkingSpots.Include(p => p.ParkingLot).ToListAsync();

//            foreach (var r in reservation)
//            {
//                parkingSpots.Remove(r.ParkingSpot);
//            }
//            return parkingSpots;
//        }

//        //Get: Available Specific Spots
//        [Route("~/api/parkingspots/freeSpots/{entryHour}/{leaveHour}")]
//        public async Task<ActionResult<IEnumerable<ParkingSpot>>> GetParkingSpecificFreeSpots(DateTime entryHour, DateTime leaveHour)
//        {
//            if (entryHour > leaveHour)
//            {
//                return BadRequest("Can't leave before you enter");
//            }
//            var reservation = await _context.Reservations.Where(r => r.startTime <= leaveHour && r.endTime >= entryHour).Include(s => s.ParkingSpot).ThenInclude(s => s.ParkingLot).ToListAsync();
//            var parkingSpots = await _context.ParkingSpots.Include(p => p.ParkingLot).ToListAsync();


//            foreach (var r in reservation)
//            {
//                parkingSpots.Remove(r.ParkingSpot);
//            }
//            return parkingSpots;
//        }

//        //Get: Available Parking Spots by price
//        [Route("~/api/parkingspots/freeSpots/{price}")]

//        public async Task<ActionResult<IEnumerable<ParkingSpot>>> GetParkingPriceFreeSpots(Decimal price)
//        {
//            if (price <= 0)
//            {
//                return BadRequest("We dont sell stuff for free");
//            }
//            var reservation = await _context.Reservations.Where(r => r.startTime <= DateTime.Now && r.endTime >= DateTime.Now).Include(s => s.ParkingSpot).ThenInclude(s => s.ParkingLot).ToListAsync();
//            var parkingSpots = await _context.ParkingSpots.Where(p => p.priceHour <= price).Include(p => p.ParkingLot).ToListAsync();


//            foreach (var r in reservation)
//            {
//                parkingSpots.Remove(r.ParkingSpot);
//            }
//            return parkingSpots;
//        }

//        //Get: Available Covered Parking Spots 
//        [Route("~/api/parkingspots/freeSpots/isCovered")]

//        public async Task<ActionResult<IEnumerable<ParkingSpot>>> GetParkingCoveredFreeSpots(Boolean isCovered) {

//            var reservation = await _context.Reservations.Where(r => r.startTime <= DateTime.Now && r.endTime >= DateTime.Now).Include(s => s.ParkingSpot).ThenInclude(s => s.ParkingLot).ToListAsync();
//            var parkingSpots = await _context.ParkingSpots.Where(c => c.isCovered == true).Include(p => p.ParkingLot).ToListAsync();


//            foreach (var r in reservation) {
//                parkingSpots.Remove(r.ParkingSpot);
//            }
//            return parkingSpots;
//        }

//        // PUT: api/ParkingSpots/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutParkingSpot(string id, ParkingSpot parkingSpot)
//        {
//            if (id != parkingSpot.parkingSpotID || !ModelState.IsValid)
//            {
//                return BadRequest();
//            }

//            _context.Entry(parkingSpot).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!ParkingSpotExists(id))
//                {
//                    return NotFound("Can't Update a Parking Spot that does not Exist");
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/ParkingSpots
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<ParkingSpot>> PostParkingSpot(ParkingSpot parkingSpot)
//        {
//            _context.ParkingSpots.Add(parkingSpot);
//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateException)
//            {
//                if (ParkingSpotExists(parkingSpot.parkingSpotID))
//                {
//                    return Conflict("Parking Spot already Exist");
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            return CreatedAtAction("GetParkingSpot", new { id = parkingSpot.parkingSpotID }, parkingSpot);
//        }

//        // DELETE: api/ParkingSpots/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteParkingSpot(string id)
//        {
//            var parkingSpot = await _context.ParkingSpots.FindAsync(id);
//            if (parkingSpot == null)
//            {
//                return NotFound("Can't Delete a Parking Spot that does not Exist");
//            }

//            _context.ParkingSpots.Remove(parkingSpot);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool ParkingSpotExists(string id)
//        {
//            return _context.ParkingSpots.Any(e => e.parkingSpotID == id);
//        }
//    }
//}




using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrivateParkAPI.Contracts;
using PrivateParkAPI.Data;
using PrivateParkAPI.Models;

namespace PrivateParkAPI.Controllers {
    [Authorize]
    [Route("api/parkingspots")]
    public class ParkingSpotsController : Controller {
        private readonly IParkingSpotRepository _spotRepository;
        private readonly IMapper _mapper;

        public ParkingSpotsController(IParkingSpotRepository spotRepository, IMapper mapper) {
            _spotRepository = spotRepository;
            _mapper = mapper;
        }

        // GET: api/ParkingSpots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingSpot>>> GetParkingSpots() {
            return Ok( _spotRepository.GetParkingSpots());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingSpot>> GetParkingSpot(string id) {
            return Ok( _spotRepository.GetParkingSpot(id));
        }

        [Route("/freeSpots")]
        public ActionResult<IEnumerable<ParkingSpot>> GetParkingFreeSpots() {
            return Ok(_spotRepository.GetParkingFreeSpots());
        }

    }
}