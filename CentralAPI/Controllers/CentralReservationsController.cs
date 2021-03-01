using CentralAPI.DTO;
using CentralAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CentralAPI.Controllers
{
    //[Authorize]
    [Route("central/reservations")]
    [ApiController]
    public class CentralReservationsController : ControllerBase
    {
        private readonly ICentralReservationService _centralReservationService;
        private readonly IReservationService _reservationService;

        public CentralReservationsController(ICentralReservationService centralReservationService, IReservationService reservationService)
        {
            _centralReservationService = centralReservationService;
            _reservationService = reservationService;
        }

        //Get all Reservations from Central API.
        [HttpGet]
        public Task<ActionResult<IEnumerable<CentralReservationDTO>>> GetCentralReservations()
        {
            return _centralReservationService.GetAllCentralReservations();
        }

        [HttpGet("users/{id}")]
        public Task<ActionResult<IEnumerable<CentralReservationDTO>>> GetCentralReservationsById(string id)
        {
            return _centralReservationService.GetAllCentralReservationsById(id);
        }

        //Get a Reservation By Id from Central API.
        [HttpGet("{id}")]
        public async Task<ActionResult<CentralReservationDTO>> GetCentralReservationById(string id)
        {

            if (await CentralReservationExists(id) == false)
            {
                return NotFound("CentralReservation was not Found");
            }
            return await _centralReservationService.GetCentralReservationById(id);
        }

        //Get a Reservation By UserId from Central API.
        [HttpGet("~/reservations/users/{userid}")]
        public async Task<ActionResult<CentralReservationDTO>> GetCentralReservationByUserId(string userID) {

            if (await UserHasReservations(userID) == false) {
                return NotFound("User's CentralReservation were not Found");
            }
            return await _centralReservationService.GetCentralReservationByUserId(userID);
        }

        //Get all Reservations that are not cancelled from Central API.
        [Route("~/central/reservations/notCancelled")]
        public async Task<ActionResult<IEnumerable<CentralReservationDTO>>> GetCentralReservationsNotCancelled()
        {
            return await _centralReservationService.GetCentralReservationsNotCancelled();
        }

        //Post a Reservation in both Park API and Central API.
        [HttpPost]
        public async Task<ActionResult<CentralReservationDTO>> PostCentralReservation([FromBody] CentralReservationDTO centralReservationDTO)
        {
            try
            {
                var centralReservation = await _centralReservationService.PostCentralReservation(centralReservationDTO);
            }
            catch (Exception ex)
            {
                if (await CentralReservationExists(centralReservationDTO.reservationID) == true)
                {
                    return Conflict("The CentralReservations already exist");
                }
                return BadRequest(ex);
            }
            return CreatedAtAction("PostCentralReservation", new { id = centralReservationDTO.reservationID }, centralReservationDTO);
        }
        [HttpPost]
        [Route("~/central/reservations/notCompleted")]
        public async Task<ActionResult<CentralReservationDTO>> PostCentralReservationNotCompleted([FromBody] CentralReservationDTO centralReservationDTO)
        {
            try
            {
                var centralReservation = await _centralReservationService.PostCentralReservationNotCompleted(centralReservationDTO);
                await _reservationService.PostReservation(centralReservation.Value, centralReservationDTO.parkingLotID);
            }
            catch (Exception e)
            {
                if (await CentralReservationExists(centralReservationDTO.reservationID) == true)
                {
                    return Conflict("The CentralReservations already exist" + e);
                }
                throw;
            }
            return CreatedAtAction("PostCentralReservation", new { id = centralReservationDTO.reservationID }, centralReservationDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CentralReservationDTO>> PatchCentralReservation(string id)
        {
            var centralReservationDTO = await _centralReservationService.GetCentralReservationById(id);

            if (await CentralReservationExists(id) == true)
            {
                if (centralReservationDTO.Value.isCancelled == false)
                {
                    centralReservationDTO = await _centralReservationService.PatchCentralReservation(id);
                    await _reservationService.PatchReservation(centralReservationDTO.Value.reservationID, centralReservationDTO.Value.parkingLotID);
                    return centralReservationDTO;
                }
                return BadRequest("Couldn't change value");
            }
            return NotFound("Reservation does not exist");
        }

        [HttpPut("~/central/reservations/notCompleted/{id}")]
        public async Task<ActionResult<CentralReservationDTO>> CompleteCentralReservation(string id)
        {
            var centralReservationDTO = await _centralReservationService.GetCentralReservationById(id);

            if (CentralReservationExists(id).Result == true)
            {
                if (centralReservationDTO.Value.isCancelled == false)
                {
                    var newcentralReservationDTO = await _centralReservationService.CompleteCentralReservation(id);
                    await _reservationService.completeReservation(newcentralReservationDTO.Value);
                    return newcentralReservationDTO;
                }
                return BadRequest("Couldn't change value");
            }
            return NotFound("Reservation does not exist");
        }


        [HttpPut("~/central/reservations/sublet/{id}")]
        public async Task<ActionResult<CentralReservationDTO>> SubletCentralReservation(string id)
        {
            var centralReservationDTO = await _centralReservationService.GetCentralReservationById(id);

            if (CentralReservationExists(id).Result == true)
            {
                if (centralReservationDTO.Value.isCancelled == false)
                {
                    return await _centralReservationService.SubletCentralReservation(id);
                }
                return BadRequest("Couldn't change value");
            }
            return NotFound("Reservation does not exist");
        }
        //CentralReservation exists
        public async Task<bool> CentralReservationExists(string id)
        {
            return await _centralReservationService.FindCentralReservationAny(id);

        }

        //CentralReservation exists
        public async Task<bool> UserHasReservations(string userID) {
            return await _centralReservationService.FindCentralReservationAnyByUser(userID);

        }

        /* ------------------------------- RESERVATIONS PARK API -------------------------------*/

        //Get All Resevations
        [HttpGet]
        [Route("~/park/parkinglot/{id}/allparkreservations")]
        public async Task<ActionResult<IEnumerable<CentralReservationDTO>>> GetAllReservations(int id)
        {
            try
            {
                return await _reservationService.GetAllReservations(id);
            }
            catch (HttpRequestException ex)
            {
                if (ex.Message == "Not Found")
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }

        //Get All Not Cancelled Reservations
        [HttpGet]
        [Route("~/park/parkinglot/{id}/parkreservations")]
        public async Task<ActionResult<IEnumerable<CentralReservationDTO>>> GetAllNotCanceledReservations(int id)
        {
            return await _reservationService.GetAllNotCanceledReservations(id);
        }

        //Get Resevations by Id
        [HttpGet]
        [Route("~/park/parkinglot/{pLotID}/parkreservations/{id}")]
        public async Task<ActionResult<CentralReservationDTO>> GetReservationById(string id, int pLotID)
        {
            try
            {
                return await _reservationService.GetReservationById(id, pLotID);
            }
            catch (Exception e)
            {
                return NotFound("Reservation was not found" + e);
            }
        }
    }
}

