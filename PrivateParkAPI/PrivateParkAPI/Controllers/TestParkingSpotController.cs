using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using PrivateParkAPI.DTO;
using PrivateParkAPI.Services.IServices;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace PrivateParkAPI.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestParkingSpotController : Controller
    {
        private readonly IParkingSpotService _parkingSpotService;
        [ActivatorUtilitiesConstructor]
        public TestParkingSpotController(IParkingSpotService parkingSpotService)
        {
            _parkingSpotService = parkingSpotService;
        }
        
        public TestParkingSpotController() {

        }

        //Get not Private ParkingSpots
        [HttpGet]
        public Task<IEnumerable<ParkingSpotDTO>> GetAllNotPrive()
        {
            return _parkingSpotService.GetAllnotPrivate();
        }

        //Get All ParkingSpots (private and not Private)
        [HttpGet]
        [Route("~/api/test/all")]
        public Task<IEnumerable<ParkingSpotDTO>> GetAllParkingSpots()
        {
            return _parkingSpotService.GetAllParkingSpots();
        }

        //Get All Available ParkingSpots 
        [HttpGet]
        [Route("~/api/test/freeSpots")]
        public Task<IEnumerable<ParkingSpotDTO>> GetAllFreeParkingSpots()
        {
            return _parkingSpotService.GetFreeParkingSpots();
        }

        //Get All Available ParkingSpots in a set of dates
        [HttpGet]
        [Route("~/api/test/freeSpots/{startDate}/{endDate}")]
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
        [Route("~/api/test/freeSpots/{priceHour}")]
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
