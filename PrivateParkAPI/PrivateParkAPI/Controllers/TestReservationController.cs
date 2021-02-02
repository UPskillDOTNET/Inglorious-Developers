using Microsoft.AspNetCore.Mvc;
using PrivateParkAPI.DTO;
using PrivateParkAPI.Services.IServices;
using System;
using System.Collections.Generic;
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
        public Task<IEnumerable<ReservationDTO>> GetReservations()
        {
            return _reservationService.GetReservations();
        }

        [HttpGet("{id}")]
        public Task<ReservationDTO> GetReservation(string id)
        {
            var reservation = _reservationService.GetReservation(id);

            return reservation;
        }

        //Post Resevation
        [HttpPost]
        public async Task<IActionResult> PostReservation( ReservationDTO reservationDTO)
        {

            var Results = _reservationService.Validate(reservationDTO);
            var id = reservationDTO.reservationID;
            var TheController = new TestParkingSpotController(_parkingSpotService);

            if (!Results.IsValid)
            {
                return BadRequest("Can't create " + Results);
            }
            if (reservationDTO.ParkingSpot.isPrivate == true)
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


        //Put Reservation
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutReservation(string id, ReservationDTO reservationDTO)
        //{
        //    try
        //    {
        //        await _reservationService.PutReservation(reservationDTO.reservationID, reservationDTO);
        //    }
        //    catch (Exception)
        //    {
        //        if (await ReservationExists(id) == false)
        //        {
        //            return NotFound("The Reservation does not exist");
        //        }
        //        throw;
        //    }
        //    return NoContent();
        //}

        //Delete Reservation
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteReservation(string id)
        //{
        //    try
        //    {
        //        await _reservationService.DeleteReservation(id);
        //    }
        //    catch (Exception)
        //    {
        //        if (await ReservationExists(id) == false)
        //        {
        //            return Conflict("Can't delete an non-existing Reservation");
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //    return NotFound();
        //}

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
