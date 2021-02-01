using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicParkAPI.Data;
using PublicParkAPI.DTO;
using PublicParkAPI.Models;
using PublicParkAPI.Services;
using PublicParkAPI.Services.IServices;

namespace PublicParkAPI.Controllers
{

    [Route("api/testesReservations")]
    [ApiController]
    public class Reservations2Controller : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IParkingSpotService _parkingSpotService;

        public Reservations2Controller(IReservationService reservationService, IParkingSpotService parkingSpotService)
        {
            _reservationService = reservationService;
            _parkingSpotService = parkingSpotService;
        }

        //Get All Resevations
        [HttpGet]
        public Task<IEnumerable<ReservationDTO>> GetReservations()
        {
            return _reservationService.GetReservations();
        }

        //Get Resevation By ID
        [HttpGet("{id}")]
        public Task<ReservationDTO> GetReservation(string id)
        {
            return _reservationService.GetReservation(id);
        }

        //Put Reservation
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(string id, [FromBody]ReservationDTO reservationDTO)
        {
            try
            {
                await _reservationService.PutReservation(reservationDTO.reservationID, reservationDTO);                
            }
            catch (Exception)
            {
                if (await ReservationExists(id) == false)
                {
                    return NotFound("The Reservation was not found");
                }
                throw;
            }
            return NoContent();
        }

        //Post Resevation
        [HttpPost]
        public async Task<IActionResult> PostReservation([FromBody]ReservationDTO reservationDTO)
        {
            var id = reservationDTO.reservationID;
            var TheController = new ParkingSpots2Controller(_parkingSpotService);            

            if (await TheController.ParkingSpotExists(reservationDTO.parkingSpotID) == false)
            {
                return BadRequest("ParkingSpot doens't exist.");
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

        //Delete Reservation
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(string id)
        {
            try
            {
                await _reservationService.DeleteReservation(id);
            }
            catch (Exception)
            {
                if (await ReservationExists(id) == false)
                {
                    return Conflict("The Reservation doesn't exist");
                }
                throw;
            }
            return NotFound();
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
