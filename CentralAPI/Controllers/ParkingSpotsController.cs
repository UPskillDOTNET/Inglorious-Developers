using CentralAPI.DTO;
using CentralAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentralAPI.Controllers
{
    [Route("central/[controller]")]
    [ApiController]
    public class ParkingSpotsController : ControllerBase
    {
        private readonly IParkingLotService _parkingLotService;
        private readonly IParkingSpotService _parkingSpotService;

        public ParkingSpotsController(IParkingLotService parkingLotService, IParkingSpotService parkingSpotService)
        {
            _parkingLotService = parkingLotService;
            _parkingSpotService = parkingSpotService;
        }


        [HttpGet("parkinglot/{pLotId}")]
        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetAllParkingSpots(int pLotId)
        {
            try
            {
                return await _parkingSpotService.GetAllParkingSpots(pLotId);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("free/parkinglot/{pLotId}")]
        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetAllFreeParkingSpots(int pLotId)
        {
            try
            {
                return await _parkingSpotService.GetAllFreeParkingSpots(pLotId);
            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        [HttpGet("free/parkinglot/{pLotId}/{startDate}/{endDate}")]
        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetFreeParkingSpotsByDate(int pLotId, DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                return BadRequest("Dates are not correct");
            }

            try
            {
                return await _parkingSpotService.GetFreeParkingSpotsByDate(startDate, endDate, pLotId);
            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        [HttpGet("free/parkinglot/{pLotId}/{priceHour}")]
        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetFreeParkingSpotsByPrice(int pLotId, Decimal priceHour)
        {

            if (priceHour <= 0)
            {
                return BadRequest("Can't input a negative price");
            }

            try
            {
                return await _parkingSpotService.GetFreeParkingSpotsByPrice(pLotId, priceHour);
            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        [HttpGet("{pSpotId}/parkinglot/{pLotId}")]
        public async Task<ActionResult<ParkingSpotDTO>> GetParkingSpotById(string pSpotId, int pLotId)
        {
            try
            {
                return await _parkingSpotService.GetParkingSpotById(pLotId, pSpotId);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost("parkinglot/{pLotId}")]
        public async Task<ActionResult<ParkingSpotDTO>> CreateParkingSpot([FromBody] ParkingSpotDTO parkingSpotDTO, int pLotId)
        {
            var Results = _parkingSpotService.Validate(parkingSpotDTO);

            if (!Results.IsValid)
            {
                return BadRequest("Can't create " + Results);
            }

            try
            {
                await _parkingSpotService.CreateParkingSpot(parkingSpotDTO, pLotId);
                return CreatedAtAction("GetParkingSpot", new { id = parkingSpotDTO.parkingSpotID }, parkingSpotDTO);
            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        [HttpPut("parkinglot/{pLotId}/parkingspot/{pSpotId}")]
        public async Task<ActionResult<ParkingSpotDTO>> EditParkingSpot(string pSpotId, [FromBody]ParkingSpotDTO parkingSpotDTO, int pLotId)
        {

            var Results = _parkingSpotService.Validate(parkingSpotDTO);
    
            if (!Results.IsValid)
            {
                return BadRequest("Can't update " + Results);
            }

            if (pSpotId != parkingSpotDTO.parkingSpotID)
            {
                return BadRequest();
            }

            try
            {
                await _parkingSpotService.EditParkingSpot(pSpotId, parkingSpotDTO, pLotId);
                return NoContent();
            }
            catch (Exception)
            {

                return NotFound();
            }
        }
    }
}


