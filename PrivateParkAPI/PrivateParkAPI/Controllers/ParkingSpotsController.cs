using Microsoft.AspNetCore.Mvc;
using PrivateParkAPI.DTO;
using PrivateParkAPI.Services.IServices;
using System;
using System.Collections.Generic;

using System.Threading.Tasks;

namespace PrivateParkAPI.Controllers
{
    [Route("api/parkingspots")]
    [ApiController]
    public class ParkingSpotsController : Controller
    {
        private readonly IParkingSpotService _parkingSpotService;

        public ParkingSpotsController(IParkingSpotService parkingSpotService)
        {
            _parkingSpotService = parkingSpotService;
        }

        //Get not Private ParkingSpots
        [HttpGet]
        public Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetAllNotPrivate()
        {
            return _parkingSpotService.GetAllnotPrivate();
        }

        //Get All ParkingSpots (private and not Private)
        [HttpGet]
        [Route("~/api/parkingspots/all")]
        public Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetAllParkingSpots()
        {
            return _parkingSpotService.GetAllParkingSpots();
        }

        //Get All Available ParkingSpots 
        [HttpGet]
        [Route("~/api/parkingspots/freeSpots")]
        public Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetFreeParkingSpots()
        {
            return _parkingSpotService.GetFreeParkingSpots();
        }

        //Get All Available ParkingSpots in a set of dates
        [HttpGet]
        [Route("~/api/parkingspots/freeSpots/{startDate}/{endDate}")]
        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetFreeParkingSpotsByDate(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                return BadRequest("Dates not correct");
            }

            return await _parkingSpotService.GetFreeParkingSpotsByDate(startDate, endDate);
        }

        //Get All Available ParkingSpots by price
        [Route("~/api/parkingspots/freeSpots/{priceHour}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetFreeParkingSpotsbyPrice(decimal priceHour)
        {
            if (priceHour <= 0)
            {
                return BadRequest("Can't input a negative price");
            }
           
            return await _parkingSpotService.GetFreeParkingSpotsbyPrice(priceHour);
        }

        //Get ParkingSpot by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingSpotDTO>> GetParkingSpot(string id)
        {

            if (await ParkingSpotExists(id) == false)
            {
                return NotFound("ParkingSpot not Found");
            }
            return await _parkingSpotService.GetParkingSpot(id);
        }

        //Update ParkingSpot 
        [HttpPut("{id}")]
        public async Task<ActionResult<ParkingSpotDTO>> PutParkingSpot(string id, ParkingSpotDTO parkingSpotDTO)
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
                    return NotFound("The Parking Spot you were trying to update could not be found");
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
        //Create ParkingSpot
        [HttpPost]
        public async Task<ActionResult<ParkingSpotDTO>> PostParkingSpot(ParkingSpotDTO parkingSpotDTO)
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
                if (await ParkingSpotExists(id) == true)
                {
                    return Conflict("ParkingSpot already exist");
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetParkingSpot", new { id = parkingSpotDTO.parkingSpotID }, parkingSpotDTO);
        }
        //Delete ParkingSpot
        [HttpDelete("{id}")]
        public async Task<ActionResult<ParkingSpotDTO>> DeleteParkingSpot(string id)
        {

            try
            {
                await _parkingSpotService.DeleteParkingSpot(id);
            }
            catch (InvalidOperationException)
            {
                if (await ParkingSpotExists(id) == false)
                {
                    return NotFound("Can't delete an non-existing ParkingSpot");
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        public async Task<bool> ParkingSpotExists(string id)
        {
            return await _parkingSpotService.FindParkingSpotAny(id);

        }
    }
}
