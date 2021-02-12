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
        private readonly IReservationService _reservationService;

        public CentralReservationsController(ICentralReservationService centralReservationService, IReservationService reservationService ) {
            _centralReservationService = centralReservationService;
            _reservationService = reservationService;
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
            try {
                await _centralReservationService.PostCentralReservation(centralReservationDTO);
                await _reservationService.PostReservation(centralReservationDTO,centralReservationDTO.parkingLotID);
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

