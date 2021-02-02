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
    public class ParkingLots2Controller : ControllerBase
    {
        private readonly IParkingLotService _parkingLotService;

        public ParkingLots2Controller(IParkingLotService parkingLotService)
        {
            _parkingLotService = parkingLotService;
        }

        // GET: api/testesParkingLot
        [HttpGet]
        public Task<IEnumerable<ParkingLotDTO>> GetParkingLots()
        {
            return _parkingLotService.GetParkingLots();

        }

        // GET: api/testesParkingLots/5
        [HttpGet("{id}")]
        public Task<ParkingLotDTO> GetParkingLot(int id)
        {
            return _parkingLotService.GetParkingLot(id);
        }

        // PUT: api/testesParkingLot/5
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
                    return NotFound("Parking Spot not found.");
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
        public async Task<IActionResult> PostParkingLot([FromBody] ParkingLotDTO parkingLotDTO)
        {
            await _parkingLotService.PostParkingLot(parkingLotDTO);
            return Ok();
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
