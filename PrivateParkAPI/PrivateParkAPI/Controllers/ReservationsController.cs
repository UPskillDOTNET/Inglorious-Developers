using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrivateParkAPI.DTO;
using PrivateParkAPI.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrivateParkAPI.Controllers
{
    [Authorize]
    [Route("api/reservations")]
    [ApiController]
    public class ReservationsController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IParkingSpotService _parkingSpotService;

        public ReservationsController(IReservationService reservationService, IParkingSpotService parkingSpotService)
        {
            _reservationService = reservationService;
            _parkingSpotService = parkingSpotService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservations()
        {
            return await _reservationService.GetReservations();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDTO>> GetReservation(string id)
        {

            if (await ReservationExists(id) == false)
            {
                return NotFound("Reservarion not Found");
            }
            return await _reservationService.GetReservation(id);
        }

        //GetAll not cancelled
        [Route("~/api/reservations/notCancelled")]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservationsNotCancelled()
        {
            return await _reservationService.GetReservationsNotCancelled();
        }

        //Post Resevation
        [HttpPost]
        public async Task<ActionResult<ReservationDTO>> PostReservation(ReservationDTO reservationDTO)
        {

            var Results = _reservationService.Validate(reservationDTO);
            var parkingSpot = await _parkingSpotService.Find(reservationDTO.parkingSpotID);

            if (!Results.IsValid)
            {
                return BadRequest("Can't create " + Results);
            }
            if (parkingSpot == null)
            {
                return BadRequest("ParkingSpot doens't exist");
            }
            if (parkingSpot.isPrivate == true)
            {
                return BadRequest("ParkingSpot is not available for reservation");
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

            if (await ReservationExists(id))
            {
                if (reservationDTO.Value.isCancelled == false)
                {
                    await _reservationService.PatchReservation(id);
                    return Ok(reservationDTO);
                }
                return BadRequest("Couldn't change value");
            }
            return NotFound("Reservation does not Exist");
        }
        [HttpPut("~/api/reservations/notCompleted/{id}")]
        public async Task<ActionResult<ReservationDTO>> completeReservation(string id, ReservationDTO reservationDTO)
        {

            if (await ReservationExists(id))
            {
                if (reservationDTO.isCancelled == false)
                {
                    var reservation = await _reservationService.completeReservation(reservationDTO);
                    return Ok(reservation.Value);
                }
                return BadRequest("Couldn't change value");
            }
            return NotFound("Reservation does not Exist");
        }

        public async Task<bool> ReservationExists(string id)
        {
            return await _reservationService.FindReservationAny(id);
        }
    }
}
