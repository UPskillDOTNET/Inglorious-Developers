using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicParkAPI.Contracts;
using PublicParkAPI.Data;
using PublicParkAPI.DTO;
using PublicParkAPI.Models;
using PublicParkAPI.Services;
using PublicParkAPI.Services.IServices;

namespace PublicParkAPI.Controllers
{

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
            var reservation = await _reservationService.GetReservation(id);
            if (reservation.Value == null)
            {
                return NotFound(reservation);
            }
            return reservation;
        }

        [Route("~/api/reservations/notCancelled")]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservationsNotCancelled()
        {
            return await _reservationService.GetReservationsNotCancelled();
        }

        [HttpPost]
        public async Task<IActionResult> PostReservation([FromBody] ReservationDTO reservationDTO)
        {
            var Results = _reservationService.Validate(reservationDTO);
            var id = reservationDTO.reservationID;
            var TheController = new ParkingSpotsController(_parkingSpotService);
            var parkingSpot = await _parkingSpotService.Find(reservationDTO.parkingSpotID);

            if (!Results.IsValid)
            {
                return BadRequest("Can't create " + Results);
            }

            if (await TheController.ParkingSpotExists(reservationDTO.parkingSpotID) == false)
            {
                return BadRequest("ParkingSpot doesn't exist.");
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
                return BadRequest("Couldn't change value");
            }
            return NotFound("Reservation does not exist");
        }
        
        //Reservation exists
        public async Task<bool> ReservationExists(string id)
        {
            var reservation = await _reservationService.GetReservation(id);

            if (reservation != null)
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
