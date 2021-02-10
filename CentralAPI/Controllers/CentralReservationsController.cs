using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using CentralAPI.Services.IServices;
using CentralAPI.DTO;
using CentralAPI.Controllers;

namespace CentralAPI.Controllers {
    [Route("api/centralreservations")]
    [ApiController]
    public class CentralReservationsController : ControllerBase {
        private readonly ICentralReservationService _centralReservationService;
        private readonly IParkingLotService _parkingLotService;
        //private readonly ReservationsController _reservationsController;
        //private readonly ParkingSpotController _parkingSpotController;

        public CentralReservationsController(ICentralReservationService centralReservationService, IParkingLotService parkingLotService /*ParkingSpotController parkingSpotController*/) {
            _centralReservationService = centralReservationService;
            _parkingLotService = parkingLotService;
            //_parkingSpotController = parkingSpotController;
            //_userService = userService;
        }

        [HttpGet]
        public Task<ActionResult<IEnumerable<CentralReservationDTO>>> GetCentralReservations() {
            return _centralReservationService.GetAllCentralReservations();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CentralReservationDTO>> GetCentralReservationById(string id) {

            if (await CentralReservationExists(id) == false) {
                return NotFound("CentralReservation was not Found");
            }
            return await _centralReservationService.GetCentralReservationById(id);
        }

        [Route("~/api/centralreservations/notCancelled")]
        public async Task<ActionResult<IEnumerable<CentralReservationDTO>>> GetCentralReservationsNotCancelled() {
            return await _centralReservationService.GetCentralReservationsNotCancelled();
        }

        [HttpPost]
        public async Task<ActionResult<CentralReservationDTO>> PostCentralReservation([FromBody] CentralReservationDTO centralReservationDTO) {
            //var Results = _centralReservationService.Validate(centralReservationDTO);
            //var parkingLotExists = _parkingLotService.GetParkingLot(centralReservationDTO.parkingLotID);
            //var parkingSpotPubExists = _parkingSpotController.GetPublicParkingSpot(centralReservationDTO.parkingSpotID);
            //var parkingSpotPriExists = _parkingSpotController.GetPrivateParkingSpot(centralReservationDTO.parkingSpotID);
            //var userExists = _userService.Find(centralReservationDTO.userID);

            //if (!Results.IsValid) {
            //    return BadRequest("Can't create " + Results);
            //}

            //if (parkingSpotPubExists == null) {
            //    return BadRequest("Public parkingSpot doesn't exist.");
            //}

            //if (parkingSpotPriExists == null) {
            //    return BadRequest("Private parkingSpot doesn't exist.");
            //}

            //if (user == null) {
            //    return BadRequest("User doesn't exist.");
            //}

            try {
                await _centralReservationService.PostCentralReservation(centralReservationDTO);
            } catch (Exception) {
                if (await CentralReservationExists(centralReservationDTO.reservationID) == true) {
                    return Conflict("The CentralReservations already exist");
                }
                throw;
            }
            return CreatedAtAction("PostCentralReservation", new { id = centralReservationDTO.reservationID }, centralReservationDTO);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<CentralReservationDTO>> PatchCentralReservation(string id) {
            var centralReservationDTO = await _centralReservationService.GetCentralReservationById(id);

            if (await CentralReservationExists(id) == true) {
                if (centralReservationDTO.Value.isCancelled == false) {
                    await _centralReservationService.PatchCentralReservation(id);
                    return Ok("CentralReservation Cancelled");
                }
                return BadRequest("Couldn't change value");
            }
            return NotFound("Reservation does not exist");
        }

        //CentralReservation exists
        public async Task<bool> CentralReservationExists(string id) {
            return await _centralReservationService.FindCentralReservationAny(id);

        }
    }

}

