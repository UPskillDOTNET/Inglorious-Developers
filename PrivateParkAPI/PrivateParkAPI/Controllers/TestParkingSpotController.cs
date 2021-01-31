using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrivateParkAPI.DTO;
using PrivateParkAPI.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrivateParkAPI.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestParkingSpotController : Controller
    {
        private readonly IParkingSpotService _parkingSpotService;

        public TestParkingSpotController(IParkingSpotService parkingSpotService)
        {
            _parkingSpotService = parkingSpotService;
        }

        [HttpGet]
        public Task<IEnumerable<ParkingSpotDTO>> GetAllNotPrive()
        {
            return _parkingSpotService.GetAllnotPrivate();
        }

        [HttpGet]
        [Route("~/api/test/all")]
        public Task<IEnumerable<ParkingSpotDTO>> GetAllParkingSpots()
        {
            return _parkingSpotService.GetAllParkingSpots();
        }

        [HttpGet]
        [Route("~/api/test/freeSpots")]
        public Task<IEnumerable<ParkingSpotDTO>> GetAllFreeParkingSpots()
        {
            return _parkingSpotService.GetFreeParkingSpots();
        }


        [HttpGet("{id}")]
        public Task<ParkingSpotDTO> GetParkingSpot(string id)
        {
            var parkingSpots = _parkingSpotService.GetParkingSpot(id);

            return parkingSpots;
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> PutParkingSpot(string id, ParkingSpotDTO parkingSpotDTO)
        {
            await _parkingSpotService.PutParkingSpot(id, parkingSpotDTO);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> PostParkingSpot(ParkingSpotDTO parkingSpotDTO)
        {
            var id = parkingSpotDTO.parkingSpotID;
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _parkingSpotService.PostParkingSpot(parkingSpotDTO);
            }
            
            catch (Exception)
            {
                if (ParkingSpotExists(id))
                {
                    return Conflict("Parking Spot already Exist");
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

            await _parkingSpotService.DeleteParkingSpot(id);

            return NotFound();
        }

        public bool ParkingSpotExists(string id)
        {
            var parkingspot =  _parkingSpotService.GetParkingSpot(id);
           
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
