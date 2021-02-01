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
using PublicParkAPI.Services.IServices;

namespace PublicParkAPI.Controllers
{

    [Route("api/testesReservations")]
    [ApiController]
    public class Reservations2Controller : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public Reservations2Controller(IReservationService reservationService)
        {
            _reservationService = reservationService;
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
        public async Task<IActionResult> PutReservation([FromBody]ReservationDTO reservationDTO)
        {
            await _reservationService.PutReservation(reservationDTO.reservationID, reservationDTO);

            return NoContent();
        }

        //Post Resevation
        [HttpPost]
        public async Task<IActionResult> PostReservation([FromBody]ReservationDTO reservationDTO)
        {
            var id = reservationDTO.reservationID;
            await _reservationService.PostReservation(reservationDTO);
            return CreatedAtAction("PostReservation", reservationDTO);
        }

        //Delete Reservation
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(string id)
        {
            await _reservationService.DeleteReservation(id);
            return Ok();
        }
    }
}
