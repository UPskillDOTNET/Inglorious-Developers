using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicParkAPI.Contracts;
using PublicParkAPI.Data;
using PublicParkAPI.DTO;
using PublicParkAPI.Models;
using PublicParkAPI.Services;

namespace PublicParkAPI.Controllers
{
    [Route("api/testes")]
    public class ParkingSpots2Controller : Controller
    {
        private readonly IParkingSpotService _parkingSpotService;

        public ParkingSpots2Controller(IParkingSpotService parkingSpotService)
        {
            _parkingSpotService = parkingSpotService;
        }


        // GET: api/ParkingSpots
        [HttpGet]
        public Task<IEnumerable<ParkingSpotDTO>> GetParkingSpots()
        {
            return _parkingSpotService.GetParkingSpots();
        }

        [HttpGet("{id}")]
        public Task<ParkingSpotDTO> GetParkingSpot(string id)
        {
            return _parkingSpotService.GetParkingSpot(id);
        }

        [HttpGet]
        [Route("~/api/test/freeSpots")]
        public Task<IEnumerable<ParkingSpotDTO>> GetFreeParkingSpots()
        {
            return _parkingSpotService.GetFreeParkingSpots();
        }

        //Get: Available Specific Spots
        [Route("~/api/testes/parkingspots/freeSpots/{entryHour}/{leaveHour}")]
        public Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetParkingSpecificFreeSpots(DateTime entryHour, DateTime leaveHour)
        {   
            return _parkingSpotService.GetParkingSpecificFreeSpots(entryHour, leaveHour);
        }

        ////Get: Available Parking Spots by price
        //[Route("~/api/testes/parkingspots/freeSpots/{price}")]

        //public async Task<ActionResult<IEnumerable<ParkingSpot>>> GetParkingPriceFreeSpots(Decimal price)
        //{
        //    if (price < 0)
        //    {
        //        return BadRequest();
        //    }
        //    var reservation = await _context.Reservations.Where(r => r.startTime <= DateTime.Now && r.endTime >= DateTime.Now).Include(s => s.ParkingSpot).ThenInclude(s => s.ParkingLot).ToListAsync();
        //    var parkingSpots = await _context.ParkingSpots.Where(p => p.priceHour <= price).Include(p => p.ParkingLot).ToListAsync();


        //    foreach (var r in reservation)
        //    {
        //        parkingSpots.Remove(r.ParkingSpot);
        //    }
        //    return parkingSpots;
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> PutParkingSpot([FromBody]ParkingSpotDTO parkingSpotDTO)
        {
            await _parkingSpotService.PutParkingSpot(parkingSpotDTO.parkingSpotID, parkingSpotDTO);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> PostParkingSpot([FromBody]ParkingSpotDTO parkingSpotDTO)
        {
            var id = parkingSpotDTO.parkingSpotID;
            await _parkingSpotService.PostParkingSpot(parkingSpotDTO);
            return CreatedAtAction("PostParkingSpot", parkingSpotDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParkingSpot(string id)
        {
            await _parkingSpotService.DeleteParkingSpot(id);
            return Ok();
        }

        public bool ParkingSpotExists(string id)
        {
            var parkingspot = _parkingSpotService.GetParkingSpot(id);
            if (parkingspot != null)
            {

                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
