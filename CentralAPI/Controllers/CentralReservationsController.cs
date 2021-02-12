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
    [Route("central/reservations")]
    [ApiController]
    public class CentralReservationsController : ControllerBase {
        private readonly ICentralReservationService _centralReservationService;
        private readonly IReservationService _reservationService;

        public CentralReservationsController(ICentralReservationService centralReservationService, IReservationService reservationService ) {
            _centralReservationService = centralReservationService;
            _reservationService = reservationService;
        }

        //Get All Central Reservations
        [HttpGet]
        public Task<ActionResult<IEnumerable<CentralReservationDTO>>> GetCentralReservations() {
            return _centralReservationService.GetAllCentralReservations();
        }

        //Get Central Reservation By Id
        [HttpGet("{id}")]
        public async Task<ActionResult<CentralReservationDTO>> GetCentralReservationById(string id) {

            if (await CentralReservationExists(id) == false) {
                return NotFound("CentralReservation was not Found");
            }
            return await _centralReservationService.GetCentralReservationById(id);
        }

        //Get All Not Canceled Reservation 
        [Route("~/central/reservations/notCancelled")]
        public async Task<ActionResult<IEnumerable<CentralReservationDTO>>> GetCentralReservationsNotCancelled() {
            return await _centralReservationService.GetCentralReservationsNotCancelled();
        }

        //Post Reservation in both Park and Central API
        [HttpPost]
        public async Task<ActionResult<CentralReservationDTO>> PostCentralReservation([FromBody] CentralReservationDTO centralReservationDTO) {
            try {
                await _reservationService.PostReservation(centralReservationDTO, centralReservationDTO.parkingLotID);
                await _centralReservationService.PostCentralReservation(centralReservationDTO);                
            } catch (Exception) {
                if (await CentralReservationExists(centralReservationDTO.reservationID) == true) {
                    return Conflict("The CentralReservations already exist");
                }
                throw;
            }
            return CreatedAtAction("PostCentralReservation", new { id = centralReservationDTO.reservationID }, centralReservationDTO);
        }

        [HttpPatch]
        [Route("~/central/parkinglot/{pLotID}/reservations/{id}")]
        public async Task<ActionResult<CentralReservationDTO>> PatchCentralReservation(string id, int pLotID) {
            var centralReservationDTO = await _centralReservationService.GetCentralReservationById(id);

            if (await CentralReservationExists(id) == true) {
                if (centralReservationDTO.Value.isCancelled == false) {
                    await _centralReservationService.PatchCentralReservation(id);
                    await _reservationService.PatchReservation(centralReservationDTO.Value.reservationID, pLotID);
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
        /* ------------------------------- RESERVATIONS PARK API -------------------------------*/

        //Get All Resevations
        [HttpGet]
        [Route("~/park/parkinglot/{id}/allparkreservations")]
        public async Task<ActionResult<IEnumerable<PrivateParkAPI.DTO.ReservationDTO>>> GetAllReservations(int id)
        {
            return await _reservationService.GetAllReservations(id);
        }

        //Get All Not Cancelled Reservations
        [HttpGet]
        [Route("~/park/parkinglot/{id}/parkreservations")]
        public async Task<ActionResult<IEnumerable<PrivateParkAPI.DTO.ReservationDTO>>> GetAllNotCanceledReservations(int id)
        {
            return await _reservationService.GetAllNotCanceledReservations(id);
        }

        //Get Resevations by Id
        [HttpGet]
        [Route("~/park/parkinglot/{pLotID}/parkreservations/{id}")]
        public async Task<ActionResult<PrivateParkAPI.DTO.ReservationDTO>> GetReservationById(string id, int pLotID)
        {
            try
            {
                return await _reservationService.GetReservationById(id, pLotID);
            }
            catch (Exception e)
            {
                return Conflict("Reservation was not found" + e);
            }
        }
    }
}

