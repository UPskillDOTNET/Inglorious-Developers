using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublicParkAPI.DTO;
using PublicParkAPI.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PublicParkAPI.Controllers
{
    [Authorize]
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
        [HttpGet]
        [Route("~/api/parkingSpots/freeSpots/{entryHour}/{leaveHour}")]
        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetFreeParkingSpotsByDate(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                return BadRequest("Dates not correct");
            }
            return await _parkingSpotService.GetFreeParkingSpotsByDate(startDate, endDate);
        }

        //Get: Available Parking Spots by price
        [HttpGet]
        [Route("~/api/parkingSpots/freeSpots/{priceHour}")]
        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetFreeParkingSpotsbyPrice(decimal priceHour)
        {
            if (priceHour <= 0)
            {
                return BadRequest("Can't input a negative price");
            }
            return await _parkingSpotService.GetFreeParkingSpotsbyPrice(priceHour);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingSpotDTO>> GetParkingSpot(string id)
        {
            if (await ParkingSpotExists(id) == false)
            {
                return NotFound("The Parking spot you were trying to update could not be found.");
            }
            return await _parkingSpotService.GetParkingSpot(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ParkingSpotDTO>> PutParkingSpot(string id, [FromBody] ParkingSpotDTO parkingSpotDTO)
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
                    return NotFound("The Parking spot you were trying to update could not be found.");
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ParkingSpotDTO>> PostParkingSpot([FromBody] ParkingSpotDTO parkingSpotDTO)
        {
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
                if (await ParkingSpotExists(parkingSpotDTO.parkingSpotID))
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
        public async Task<ActionResult<ParkingSpotDTO>> DeleteParkingSpot(string id)
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
