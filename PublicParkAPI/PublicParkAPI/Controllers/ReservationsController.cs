using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublicParkAPI.DTO;
using PublicParkAPI.Services;
using PublicParkAPI.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PublicParkAPI.Controllers
{
    //[Authorize]
    [Route("api/reservations")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IParkingSpotService _parkingSpotService;

        public ReservationsController(IReservationService reservationService, IParkingSpotService parkingSpotService)
        {
            _reservationService = reservationService;
            _parkingSpotService = parkingSpotService;
        }

        [HttpGet]
        public Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservations()
        {
            return _reservationService.GetReservations();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDTO>> GetReservation(string id)
        {

            if (await ReservationExists(id) == false)
            {
                return NotFound("Reservation was not Found");
            }
            return await _reservationService.GetReservation(id);
        }

        [Route("~/api/reservations/notCancelled")]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservationsNotCancelled()
        {
            return await _reservationService.GetReservationsNotCancelled();
        }

        [HttpPost]
        public async Task<ActionResult<ReservationDTO>> PostReservation([FromBody] ReservationDTO reservationDTO)
        {
            var Results = _reservationService.Validate(reservationDTO);
            var parkingSpotExists = _parkingSpotService.Find(reservationDTO.parkingSpotID);

            if (!Results.IsValid)
            {
                return BadRequest("Can't create " + Results);
            }

            if (parkingSpotExists == null)
            {
                return BadRequest("ParkingSpot doesn't exist.");
            }

            try
            {
                await _reservationService.PostReservation(reservationDTO);
            }
            catch (Exception)
            {
                if (await ReservationExists(reservationDTO.reservationID) == true)
                {
                    return Conflict("The Reservations already exist");
                }
                throw;
            }
            return CreatedAtAction("PostReservation", new { id = reservationDTO.reservationID }, reservationDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReservationDTO>> PatchReservation(string id)
        {
            var reservationDTO = await _reservationService.GetReservation(id);

            if (await ReservationExists(id) == true)
            {
                if (reservationDTO.Value.isCancelled == false)
                {
                    reservationDTO = await _reservationService.PatchReservation(id);
                    return Ok(reservationDTO);
                }
                return BadRequest("Couldn't change value");
            }
            return NotFound("Reservation does not exist");
        }

        //Reservation exists
        public async Task<bool> ReservationExists(string id)
        {
            return await _reservationService.FindReservationAny(id);

        }
    }
}
