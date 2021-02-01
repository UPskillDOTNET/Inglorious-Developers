using Microsoft.AspNetCore.Mvc;
using PrivateParkAPI.DTO;
using PrivateParkAPI.Models;
using PrivateParkAPI.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateParkAPI.Controllers
{
    [Route("api/reservationtest")]
    [ApiController]
    public class TestReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IParkingSpotService _parkingSpotService;

        public TestReservationController(IReservationService reservationService, IParkingSpotService parkingSpotService)
        {
            _reservationService = reservationService;
            _parkingSpotService = parkingSpotService;
        }

        [HttpGet]
        public  Task<IEnumerable<ReservationDTO>> GetReservations()
        {
            return  _reservationService.GetReservations();
        }

        [HttpGet("{id}")]
        public Task<ReservationDTO> GetReservation(string id) {
            var reservation = _reservationService.GetReservation(id);

            return reservation;
        }

        public bool ReservationExists(string id) {
            var reservation = _reservationService.GetReservation(id);

            if (reservation != null) {

                return true;
            } else {
                return false;
            }
        }

        //Post Resevation
        [HttpPost]
        public async Task<IActionResult> PostReservation([FromBody] ReservationDTO reservationDTO) {
            var id = reservationDTO.reservationID;
            await _reservationService.PostReservation(reservationDTO);
            return CreatedAtAction("PostReservation", reservationDTO);
        }

        //Put Reservation
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation([FromBody] ReservationDTO reservationDTO) {
            await _reservationService.PutReservation(reservationDTO.reservationID, reservationDTO);

            return NoContent();
        }
    }
}
