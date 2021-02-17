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
        //[Route("central/parkingspots/parkinglot/{id}")]
        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetAllParkingSpots(int pLotId)
        {
            return await _parkingSpotService.GetAllParkingSpots(pLotId);
        }

        [HttpGet("free/parkinglot/{pLotId}")]
        //[Route("central/freeparkingspots/parkinglot/{id}")]
        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetAllFreeParkingSpots(int pLotId)
        {
            if (await ParkingLotExists(pLotId) == false)
            {
                return NotFound("Parking Lot was not found");
            }
            return await _parkingSpotService.GetAllFreeParkingSpots(pLotId);
        }

        [HttpGet("free/parkinglot/{pLotId}/{startDate}/{endDate}")]
        //[Route("central/parkinglot/{id}/freeparkingspots/{startDate}/{endDate}")]
        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetFreeParkingSpotsByDate(int pLotId, DateTime startDate, DateTime endDate)
        {
            if (await ParkingLotExists(pLotId) == false)
            {
                return NotFound("Parking Lot was not found");
            }
            if (startDate > endDate)
            {
                return BadRequest("Dates are not correct");
            }
            return await _parkingSpotService.GetFreeParkingSpotsByDate(startDate, endDate, pLotId);
        }
        [HttpGet("free/parkinglot/{pLotId}/{priceHour}")]
        //[Route("central/parkinglot/{id}/freeparkingspots/{priceHour}")]
        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetFreeParkingSpotsByPrice(int pLotId, Decimal priceHour)
        {
            if (await ParkingLotExists(pLotId) == false)
            {
                return NotFound("Parking Lot was not found");
            }
            if (priceHour <= 0)
            {
                return BadRequest("Can't input a negative price");
            }
            return await _parkingSpotService.GetFreeParkingSpotsByPrice(pLotId, priceHour);
        }

        //GetByIdPrivate
        [HttpGet("{pSpotId}/parkinglot/{pLotId}")]
        //[Route("central/parkinglot/{pLotId}/parkingspots/{id}")]
        public async Task<ActionResult<ParkingSpotDTO>> GetParkingSpotById(string pSpotId, int pLotId)
        {
            if (await ParkingLotExists(pLotId) == false)
            {
                return NotFound("Parking Lot was not found");
            }
            if (pSpotId == null)
            {
                return NotFound();
            }
            return await _parkingSpotService.GetParkingSpotById(pLotId, pSpotId);
        }

        //POST private
        [HttpPost]
        [Route("central/parkinglot/{id}/parkingspots")]
        public async Task<ActionResult<ParkingSpotDTO>> CreateParkingSpot([Bind("parkingSpotID, priceHour, floor, isPrivate, isCovered, parkingLotID")] ParkingSpotDTO parkingSpotDTO, int id)
        {
            var Results = _parkingSpotService.Validate(parkingSpotDTO);
            if (!Results.IsValid)
            {
                return BadRequest("Can't create " + Results);
            }
            await _parkingSpotService.CreateParkingSpot(parkingSpotDTO, id);
            return CreatedAtAction("GetParkingSpot", new { id = parkingSpotDTO.parkingSpotID }, parkingSpotDTO);
        }

        //PUT private
        [HttpPut]
        [Route("central/parkinglot/{pLotId}/parkingspots/{id}")]
        public async Task<ActionResult<ParkingSpotDTO>> EditParkingSpot(string id, [Bind("parkingSpotID, priceHour, floor, isPrivate, isCovered, parkingLotID")] ParkingSpotDTO parkingSpotDTO, int pLotId)
        {

            var Results = _parkingSpotService.Validate(parkingSpotDTO);
            if (await ParkingLotExists(pLotId) == false)
            {
                return NotFound("Parking Lot was not found");
            }
            if (!Results.IsValid)
            {
                return BadRequest("Can't update " + Results);
            }

            if (id != parkingSpotDTO.parkingSpotID)
            {
                return BadRequest();
            }
            await _parkingSpotService.EditParkingSpot(id, parkingSpotDTO, pLotId);
            return NoContent();
        }

        private async Task<bool> ParkingLotExists(int id)
        {
            var parkingLot = await _parkingLotService.GetParkingLot(id);

            if (parkingLot.Value != null)
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


