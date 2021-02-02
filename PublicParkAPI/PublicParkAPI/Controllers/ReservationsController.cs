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
        private readonly IReservationRepository _reservationRepository;

        public ReservationsController(IReservationService reservationService, IParkingSpotService parkingSpotService, IReservationRepository reservationRepository)
        {
            _reservationService = reservationService;
            _parkingSpotService = parkingSpotService;
            _reservationRepository = reservationRepository;
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

        //// Needs to be a patch, isCancelled from false to true
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutReservation(string id, [FromBody]ReservationDTO reservationDTO)
        //{
        //    try
        //    {
        //        await _reservationService.PutReservation(reservationDTO.reservationID, reservationDTO);                
        //    }
        //    catch (Exception)
        //    {
        //        if (await ReservationExists(id) == false)
        //        {
        //            return NotFound("The Reservation was not found");
        //        }
        //        throw;
        //    }
        //    return NoContent();
        //}

        [HttpPatch("{id}")]
        public IActionResult PatchReservation(string id, [FromBody] JsonPatchDocument<Reservation> patchEntity)
        {
            var reservation = _reservationRepository.GetReservation(id).Result;
          
            if (reservation == null)
            {
                return NotFound();
            }

            if (reservation.isCancelled == true)
            {
                return BadRequest("This reservations is already cancelled");
            }

            reservation.isCancelled = true;
            //patchEntity.ApplyTo(reservation, ModelState);
            _reservationRepository.UpdateAsync(reservation);
            return Ok(reservation);
        }
        

        //Post Resevation
        [HttpPost]
        public async Task<IActionResult> PostReservation([FromBody]ReservationDTO reservationDTO)
        {
            var id = reservationDTO.reservationID;
            var TheController = new ParkingSpotsController(_parkingSpotService);            

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
