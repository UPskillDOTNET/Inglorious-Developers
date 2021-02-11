using CentralAPI.DTO;
using CentralAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentralAPI.Controllers
{

    [ApiController]
    public class ReservationsController : ControllerBase
    {


        private readonly IReservationService _reservationService;
        private readonly IParkingLotService _parkingLotService;


        public ReservationsController(IReservationService reservationService, IParkingLotService parkingLotService)
        {
            _reservationService = reservationService;
            _parkingLotService = parkingLotService;
        }


        //Get All Resevations
        [HttpGet]
        [Route("centralapi/parkinglot/{id}/allreservations")]
        public async Task<ActionResult<IEnumerable<PrivateParkAPI.DTO.ReservationDTO>>> GetAllReservations(int id)
        {
            if (await ParkingLotExists(id) == false)
            {
                return NotFound("Parking Lot was not found");
            }
            return await _reservationService.GetAllReservations(id);
        }


        //Get All Not Cancelled Reservations
        [HttpGet]
        [Route("centralapi/parkinglot/{id}/reservations")]
        public async Task<ActionResult<IEnumerable<PrivateParkAPI.DTO.ReservationDTO>>> GetAllNotCanceledReservations(int id)
        {
            if (await ParkingLotExists(id) == false)
            {
                return NotFound("Parking Lot was not found");
            }
            return await _reservationService.GetAllNotCanceledReservations(id);
        }

        //Get Resevations by Id
        [HttpGet]
        [Route("centralapi/parkinglot/{pLotID}/reservations/{id}")]
        public async Task<ActionResult<PrivateParkAPI.DTO.ReservationDTO>> GetReservationById(string id, int pLotID)
        {
            
            if (await ParkingLotExists(pLotID) == false)
            {
                return NotFound("Parking Lot was not found");
            }
            try
            {
                return await _reservationService.GetReservationById(id, pLotID);
            }
            catch (Exception e)
            {
                return Conflict("Reservation was not found");
            }
        }


        //POST Resevation only in ParkAPI
        [HttpPost]
        [Route("centralapi/parkinglot/{pLotID}/apireservations")]
        public async Task<ActionResult<PrivateParkAPI.DTO.ReservationDTO>> PostReservation([FromBody] PrivateParkAPI.DTO.ReservationDTO reservationDTO, int pLotID)
        {
            if (await ParkingLotExists(pLotID) == false)
            {
                return NotFound("Parking Lot was not found");
            }            
            await _reservationService.PostReservation(reservationDTO, pLotID);
            return CreatedAtAction("PostReservation", new { id = reservationDTO.reservationID }, reservationDTO);
        }

        //POST Reservation in Both Central and Park APIs
        [HttpPost]
        [Route("centralapi/parkinglot/{pLotID}/reservations")]
        public async Task<ActionResult<CentralReservationDTO>> PostUserReservation([FromBody] CentralReservationDTO reservationDTO, int pLotID)
        {
            if (await ParkingLotExists(pLotID) == false)
            {
                return NotFound("Parking Lot was not found");
            }
            await _reservationService.PostUserReservation(reservationDTO, pLotID);
            return CreatedAtAction("PostReservation", new { id = reservationDTO.reservationID }, reservationDTO);
        }

        //POST Cancel Reservation by ID
        [HttpPost]
        [Route("centralapi/parkinglot/{pLotID}/cancelreservations/{id}")]
        public async Task<ActionResult<PrivateParkAPI.DTO.ReservationDTO>> PatchReservation(string id, int pLotID)
        {
            if (await ParkingLotExists(pLotID) == false)
            {
                return NotFound("Parking Lot was not found");
            }
            var reservationDTO = await _reservationService.GetReservationById(id, pLotID);
            await _reservationService.PatchReservation(id, pLotID);
            return Ok(reservationDTO);

        }
        private async Task<bool> ParkingLotExists(int id)
        {
            var parkingLot = await _parkingLotService.GetParkingLot(id);

            if (parkingLot.Value != null)
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
