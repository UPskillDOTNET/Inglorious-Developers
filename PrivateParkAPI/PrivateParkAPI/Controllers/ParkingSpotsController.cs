using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PrivateParkAPI.DTO;
using PrivateParkAPI.Services.IServices;
using PrivateParkAPI.Utils;
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
        public Task<IEnumerable<ParkingSpotDTO>> GetAllNotPrive()
        {
            return _parkingSpotService.GetAllnotPrivate();
        }

        //Get All ParkingSpots (private and not Private)
        [HttpGet]
        [Route("~/api/parkingspots/all")]
        public Task<IEnumerable<ParkingSpotDTO>> GetAllParkingSpots()
        {
            return _parkingSpotService.GetAllParkingSpots();
        }

        //Get All Available ParkingSpots 
        [HttpGet]
        [Route("~/api/parkingspots/freeSpots")]
        public Task<IEnumerable<ParkingSpotDTO>> GetAllFreeParkingSpots()
        {
            return _parkingSpotService.GetFreeParkingSpots();
        }

        //Get All Available ParkingSpots in a set of dates
        [HttpGet]
        [Route("~/api/parkingspots/freeSpots/{startDate}/{endDate}")]
        public async Task<IActionResult> GetFreeParkingSpotsByDate(DateTime startDate, DateTime endDate)
        {
            if (startDate>endDate)
            {
                return BadRequest("Dates not correct");
            }
            var parkingSpots = await _parkingSpotService.GetFreeParkingSpotsByDate(startDate, endDate);
            return Ok(parkingSpots); 
        }

        //Get All Available ParkingSpots by price
        [Route("~/api/parkingspots/freeSpots/{priceHour}")]
        public async Task<IActionResult> GetFreeParkingSpotsbyPrice(decimal priceHour)
        {
            if (priceHour <= 0)
            {
                return BadRequest("Can't input a negative price");
            }
            var parkingSpots = await _parkingSpotService.GetFreeParkingSpotsbyPrice(priceHour);
            return Ok(parkingSpots);
        }

        //Get ParkingSpot by ID
        [HttpGet("{id}")]
        public Task<ParkingSpotDTO> GetParkingSpot(string id)
        {
            var parkingSpots = _parkingSpotService.GetParkingSpot(id);

            return parkingSpots;
        }

        //Update ParkingSpot 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParkingSpot(string id, ParkingSpotDTO parkingSpotDTO)
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
                    return NotFound("The parkingSpot you were trying to update could not be found");
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
        public async Task<IActionResult> PostParkingSpot(ParkingSpotDTO parkingSpotDTO)
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
        public async Task<IActionResult> DeleteParkingSpot(string id)
        {
            try
            {
                await _parkingSpotService.DeleteParkingSpot(id);
            }
            catch (InvalidOperationException)
            {
                if (await ParkingSpotExists(id) == false)
                {
                    return Conflict("Can't delete an non-existing ParkingSpot");
                }
                else
                {
                    throw;
                }
            }
            return NotFound();
        }

        public async Task<bool> ParkingSpotExists(string id)
        {
            var parkingspot = await  _parkingSpotService.GetParkingSpot(id);
           
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
