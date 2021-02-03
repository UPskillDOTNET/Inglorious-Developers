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
    [Route("api/parkingSpots")]
    [ApiController]
    public class ParkingSpotsController : Controller
    {
        private readonly IParkingSpotService _parkingSpotService;

        public ParkingSpotsController(IParkingSpotService parkingSpotService)
        {
            _parkingSpotService = parkingSpotService;
        }

        // GET: api/ParkingSpots
        [HttpGet]
        public Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetAllParkingSpots()
        {
            return _parkingSpotService.GetAllParkingSpots();
        }

        [HttpGet]
        [Route("~/api/parkingSpots/freeSpots")]
        public Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetFreeParkingSpots()
        {
            return _parkingSpotService.GetFreeParkingSpots();
        }

        //Get: Available Specific Spots
        [Route("~/api/parkingSpots/freeSpots/{entryHour}/{leaveHour}")]
        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetFreeParkingSpotsByDate(DateTime startDate, DateTime endDate)
        {
            if( startDate > endDate)
            {
                return BadRequest("Dates not correct");
            }
            return await _parkingSpotService.GetFreeParkingSpotsByDate(startDate, endDate);
        }

        //Get: Available Parking Spots by price
        [Route("~/api/parkingSpots/freeSpots/{priceHour}")]
        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetFreeParkingSpotsbyPrice(decimal priceHour)
        {
            if (priceHour <= 0)
            {
                return BadRequest("Can't input a negative price");
            }
            var parkingSpots = await _parkingSpotService.GetFreeParkingSpotsbyPrice(priceHour);
            return Ok(parkingSpots);
        }

        [HttpGet("{id}")]
        public Task<ActionResult<ParkingSpotDTO>> GetParkingSpot(string id)
        {
            return _parkingSpotService.GetParkingSpot(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutParkingSpot(string id, [FromBody] ParkingSpotDTO parkingSpotDTO)
        {
            var Results = _parkingSpotService.Validate(parkingSpotDTO);

            if (!Results.IsValid)
            {
                return BadRequest("Can't update " + Results);
            }

            if (id != parkingSpotDTO.parkingSpotID)
            {
                return BadRequest();
            } 
            try
            {
                await _parkingSpotService.PutParkingSpot(id, parkingSpotDTO);
            }
            catch (Exception)
            {
                if (await ParkingSpotExists(id) == false)
                {
                    return NotFound("The Parking pot you were trying to update could not be found.");
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> PostParkingSpot([FromBody] ParkingSpotDTO parkingSpotDTO)
        {
            var id = parkingSpotDTO.parkingSpotID;

            var Results = _parkingSpotService.Validate(parkingSpotDTO);

            if (!Results.IsValid)
            {
                return BadRequest("Can't create " + Results);
            }

            try
            {
                await _parkingSpotService.PostParkingSpot(parkingSpotDTO);
            }

            catch (Exception)
            {
                if (await ParkingSpotExists(id))
                {
                    return Conflict("Parking Spot already exists.");
                }
                else
                {
                    throw;
                }

            }
            return CreatedAtAction("GetParkingSpot", new { id = parkingSpotDTO.parkingSpotID }, parkingSpotDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParkingSpot(string id)
        {

            if (await ParkingSpotExists(id) == false)
            {
                return NotFound("ParkingSpot does not exist.");
            }
            else
            {
                await _parkingSpotService.DeleteParkingSpot(id);
            }
            return Ok();
        }

        public async Task<bool> ParkingSpotExists(string id)
        {
            return await _parkingSpotService.FindParkingSpotAny(id);
        }
    }
}
