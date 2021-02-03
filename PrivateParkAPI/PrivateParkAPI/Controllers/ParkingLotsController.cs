using Microsoft.AspNetCore.Mvc;
using PrivateParkAPI.DTO;
using PrivateParkAPI.Models;
using PrivateParkAPI.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateParkAPI.Controllers
{
    [Route("api/parkinglots")]
    [ApiController]
    public class ParkingLotsController : Controller
    {
        private readonly IParkingLotService _parkingLotService;

        public ParkingLotsController(IParkingLotService parkingLotService)
        {
            _parkingLotService = parkingLotService;
        }

        [HttpGet]
        public Task<ActionResult<IEnumerable<ParkingLotDTO>>> GetParkingLots()
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
                return NotFound(parkingLot);
            }
            return parkingLot;
        }

        // PUT: api/testesParkingLot/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ParkingLotDTO>> PutParkingLot(int id, ParkingLotDTO parkingLotDTO)
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

        // POST: api/testesParkingLot
        [HttpPost]
        public async Task<ActionResult<ParkingLotDTO>> PostParkingLot(ParkingLotDTO parkingLotDTO)
        {
            var id = parkingLotDTO.parkingLotID;

            var Results = _parkingLotService.Validate(parkingLotDTO);

            if (!Results.IsValid)
            {
                return BadRequest("Can't update " + Results);
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