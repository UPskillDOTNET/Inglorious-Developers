using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicParkAPI.Data;
using PublicParkAPI.DTO;
using PublicParkAPI.Models;
using PublicParkAPI.Services.IServices;

namespace PublicParkAPI.Controllers
{

    [Route("api/testesParkingLots")]
    [ApiController]
    public class ParkingLotsController : ControllerBase
    {
        private readonly IParkingLotService _parkingLotService;

        public ParkingLotsController(IParkingLotService parkingLotService)
        {
            _parkingLotService = parkingLotService;
        }

        // GET: api/testesParkingLots
        [HttpGet]
        public Task<IEnumerable<ParkingLotDTO>> GetParkingLots()
        {
            return _parkingLotService.GetParkingLots();

        }

        // GET: api/testesParkingLots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingLotDTO>> GetParkingLot(int id)
        {
            var parkingLot = await _parkingLotService.GetParkingLot(id);

            if (parkingLot.Value == null)
            {
                return NotFound();
            }
            return parkingLot;
        }

        // PUT: api/testesParkingLots/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParkingLot(int id, [FromBody] ParkingLotDTO parkingLotDTO)
        {
            try
            {
                await _parkingLotService.PutParkingLot(id, parkingLotDTO);
            }
            catch (Exception)
            {
                if (await ParkingLotExists(id) == false)
                {
                    return NotFound("Parking Lot not found.");
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }



        // POST: api/testesParkingLots
        [HttpPost]
        public async Task<IActionResult> PostParkingLot([FromBody] ParkingLotDTO parkingLotDTO)
        {
            var id = parkingLotDTO.parkingLotID;

            var Results = _parkingLotService.Validate(parkingLotDTO);

            if (!Results.IsValid)
            {
                return BadRequest("Can't update" + Results);
            }
            try
            {
                await _parkingLotService.PostParkingLot(parkingLotDTO);
            }
            catch (Exception)
            {
                if (await ParkingLotExists(id))
                {
                    return Conflict("Parking Lot already exists.");
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetParkingLot", new { id = parkingLotDTO.parkingLotID }, parkingLotDTO);
        }

        private async Task<bool> ParkingLotExists(int id)
        {
            var parkingLot = await _parkingLotService.GetParkingLot(id);

            if (parkingLot != null)
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
