using Microsoft.AspNetCore.Mvc;
using PrivateParkAPI.DTO;
using PrivateParkAPI.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrivateParkAPI.Controllers
{
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
            var reservation = await _reservationService.GetReservation(id);
            if (reservation.Value == null)
            {
                return NotFound(reservation);
            }
            return reservation;
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
            var id = reservationDTO.reservationID;
            var TheController = new ParkingSpotsController(_parkingSpotService);
            var parkingSpot = await _parkingSpotService.Find(reservationDTO.parkingSpotID);

            if (!Results.IsValid)
            {
                return BadRequest("Can't create " + Results);
            }
            if (parkingSpot.isPrivate == true)
            {
                return BadRequest("ParkingSpot is not available for reservation");
            }
            if (await TheController.ParkingSpotExists(reservationDTO.parkingSpotID) == false)
            {
                return BadRequest("ParkingSpot doens't exist");
            }
            try
            {
                await _reservationService.PostReservation(reservationDTO);
            }
            catch (Exception)
            {
                if (await ReservationExists(id) == true)
                {
                    return Conflict("The Reservations already exist");
                }
                throw;
            }
            return CreatedAtAction("PostReservation", new { id = reservationDTO.reservationID }, reservationDTO);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ReservationDTO>> PatchReservation(string id)
        {
            var reservationDTO = await _reservationService.GetReservation(id);


            if (await ReservationExists(id))
            {
                if (reservationDTO.Value.isCancelled == false)
                {
                    await _reservationService.PatchReservation(id);

                    if (reservationDTO.Value.isCancelled == true)
                    {
                        return Ok(reservationDTO);
                    }

                }
                BadRequest("Couldn't change value");

            }

            return NotFound("Reservation does not Exist"); 
        }

        public async Task<bool> ReservationExists(string id)
        {
            var reservation = await _reservationService.GetReservation(id);

            if (reservation.Result != null)
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
