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
    public class ParkingSpotsController : Controller
    {
        private readonly IParkingSpotService _parkingSpotService;

        public ParkingSpotsController(IParkingSpotService parkingSpotService)
        {
            _parkingSpotService = parkingSpotService;
        }

        // GET: api/ParkingSpots
        [HttpGet]
        public Task<IEnumerable<ParkingSpotDTO>> GetAllParkingSpots()
        {
            return _parkingSpotService.GetAllParkingSpots();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingSpotDTO>> GetParkingSpot(string id)
        {
            var parkingSpots = await _parkingSpotService.GetParkingSpot(id);
            if (parkingSpots.Value == null)
            {
                return NotFound();
            }

            return parkingSpots;
        }

        [HttpGet]
        [Route("~/api/parkingSpots/freeSpots")]
        public Task<IEnumerable<ParkingSpotDTO>> GetFreeParkingSpots()
        {
            return _parkingSpotService.GetFreeParkingSpots();
        }

        //Get: Available Specific Spots
        [Route("~/api/parkingSpots/freeSpots/{entryHour}/{leaveHour}")]
        public Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetFreeParkingSpotsByDate(DateTime entryHour, DateTime leaveHour)
        {
            return _parkingSpotService.GetFreeParkingSpotsByDate(entryHour, leaveHour);
        }

        //Get: Available Parking Spots by price
        [Route("~/api/parkingSpots/freeSpots/{price}")]
        public Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetParkingPriceFreeSpots(Decimal price)
        {
            return _parkingSpotService.GetParkingPriceFreeSpots(price);
        }

        // Deveria ser um patch, pois só é possivel alterar o preço
        // Responsabilidade do Admin

        [HttpPut("{id}")]
        public async Task<IActionResult> PutParkingSpot(string id, [FromBody] ParkingSpotDTO parkingSpotDTO)
        {
            var Results = _parkingSpotService.Validate(parkingSpotDTO);
            if (!Results.IsValid)
            {
                return BadRequest("Can't create" + Results);
            }
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
        public async Task<ActionResult<ParkingSpotDTO>> PostParkingSpot([FromBody] ParkingSpotDTO parkingSpotDTO)
        {

            var id = parkingSpotDTO.parkingSpotID;
            var Results = _parkingSpotService.Validate(parkingSpotDTO);

            if (!Results.IsValid)
            {
                return BadRequest("Can't create" + Results);
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
            var parkingspot = await _parkingSpotService.GetParkingSpot(id);

            if (parkingspot.Value != null)
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
