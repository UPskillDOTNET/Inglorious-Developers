using CentralAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PrivateParkAPI.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentralAPI.Controllers {
    [ApiController]
    public class ParkingSpotController : ControllerBase {
        //Please don't delete, a method is using this!
        private readonly IConfiguration _configure;
        private readonly IParkingLotService _parkingLotService;
        private readonly IParkingSpotService _parkingSpotService;
        private IConfiguration configure;
        private IParkingLotService parkingLotService;

        public ParkingSpotController(IConfiguration configuration, IParkingLotService parkingLotService, IParkingSpotService parkingSpotService) {
            _configure = configuration;
            _parkingLotService = parkingLotService;
            _parkingSpotService = parkingSpotService;

        }


        
        [HttpGet]
        [Route("centralapi/parkinglot/{id}/parkingspots")]

        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetAllParkingSpots(int id) {
            if (await ParkingLotExists(id) == false)
            {
                return NotFound("Parking Lot was not found");
            }
            return await _parkingSpotService.GetAllParkingSpots(id);
        }

        [HttpGet]
        [Route("centralapi/parkinglot/{id}/freeparkingspots")]
        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetAllFreeSpots(int id) {
            if (await ParkingLotExists(id) == false)
            {
                return NotFound("Parking Lot was not found");
            }
            return await _parkingSpotService.GetAllFreeSpots(id);
        }

        [HttpGet]
        [Route("centralapi/parkinglot/{id}/freeparkingspots/{entryHour}/{leaveHour}")]
        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetFreeParkingSpotsByDate(DateTime startDate, DateTime endDate, int id) {
            if (await ParkingLotExists(id) == false)
            {
                return NotFound("Parking Lot was not found");
            }
            if (startDate > endDate) {
                return BadRequest("Dates are not correct");
            }
            return await _parkingSpotService.GetFreeParkingSpotsByDate(startDate, endDate, id);
        }

        [Route("centralapi/parkinglot/{id}/freeparkingspots/{priceHour}")]
        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetFreeParkingSpotsByPrice(int id, Decimal priceHour) {
            if (await ParkingLotExists(id) == false)
            {
                return NotFound("Parking Lot was not found");
            }
            if (priceHour <= 0) {
                return BadRequest("Can't input a negative price");
            }
            return await _parkingSpotService.GetFreeParkingSpotsByPrice(id, priceHour);
        }

        //GetByIdPrivate
        [HttpGet]
        [Route("centralapi/parkinglot/{pLotId}/parkingspots/{id}")]
        public async Task<ActionResult<ParkingSpotDTO>> GetParkingSpotById(int pLotId, string id) {
            if (await ParkingLotExists(pLotId) == false)
            {
                return NotFound("Parking Lot was not found");
            }
            if (id == null) {
                return NotFound();
            }
            return await _parkingSpotService.GetParkingSpotById(pLotId, id);
        }

        //POST private
        [HttpPost]
        [Route("centralapi/parkinglot/{id}/parkingspots")]

        public async Task<ActionResult<ParkingSpotDTO>> CreateParkingSpot([Bind("parkingSpotID, priceHour, floor, isPrivate, isCovered, parkingLotID")] ParkingSpotDTO parkingSpotDTO, int id) {
            var Results = _parkingSpotService.Validate(parkingSpotDTO);
            if (await ParkingLotExists(id) == false)
            {
                return NotFound("Parking Lot was not found");
            }
            if (!Results.IsValid) {
                return BadRequest("Can't create " + Results);
            }
            await _parkingSpotService.CreateParkingSpot(parkingSpotDTO, id);
            return CreatedAtAction("GetParkingSpot", new { id = parkingSpotDTO.parkingSpotID }, parkingSpotDTO);
        }

        //PUT private
        [HttpPost]
        [Route("centralapi/parkinglot/{pLotId}/parkingspots/{id}")]

        public async Task<ActionResult<ParkingSpotDTO>> EditParkingSpot(string id, [Bind("parkingSpotID, priceHour, floor, isPrivate, isCovered, parkingLotID")] ParkingSpotDTO parkingSpotDTO, int pLotId) {

            var Results = _parkingSpotService.Validate(parkingSpotDTO);
            if (await ParkingLotExists(pLotId) == false)
            {
                return NotFound("Parking Lot was not found");
            }
            if (!Results.IsValid) {
                return BadRequest("Can't update " + Results);
            }

            if (id != parkingSpotDTO.parkingSpotID) {
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


