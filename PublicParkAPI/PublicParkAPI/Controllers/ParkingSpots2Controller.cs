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

        //Get: Available Parking Spots by price
        [Route("~/api/testes/parkingspots/freeSpots/{price}")]
        public Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetParkingPriceFreeSpots(Decimal price)
        {
            return _parkingSpotService.GetParkingPriceFreeSpots(price);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutParkingSpot(string id, [FromBody]ParkingSpotDTO parkingSpotDTO)
        {
            try
            {
                await _parkingSpotService.PutParkingSpot(id, parkingSpotDTO);
            }
            catch (Exception)
            {
                if (await ParkingSpotExists(id) == false)
                {
                    return NotFound("Parking Spot not found.");
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> PostParkingSpot([FromBody]ParkingSpotDTO parkingSpotDTO)
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
     
            if (await ParkingSpotExists(id) == false) {
                return NotFound("ParkingSpot does not exist.");
            } else
            {
                await _parkingSpotService.DeleteParkingSpot(id);
            }
            return Ok();
        }

        public async Task<bool> ParkingSpotExists(string id)
        {
            var parkingspot = await _parkingSpotService.GetParkingSpot(id);

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
