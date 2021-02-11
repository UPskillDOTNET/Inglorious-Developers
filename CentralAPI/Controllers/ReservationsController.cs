using CentralAPI.DTO;
using CentralAPI.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CentralAPI.Controllers;

namespace CentralAPI.Controllers
{

    [ApiController]
    public class ReservationsController : ControllerBase
    {

        
        private readonly IReservationService _reservationService;
        

        public ReservationsController(IReservationService reservationService)
        {            
            _reservationService = reservationService;
        }
       

        //Get All Resevations
        [HttpGet]
        [Route("centralapi/parkinglot/{id}/allreservation")]
        public async Task<ActionResult<IEnumerable<PrivateParkAPI.DTO.ReservationDTO>>> GetAllReservations(int id)
        {
            return await _reservationService.GetAllReservations(id);
        }


        //Get All Not Cancelled Reservations
        [HttpGet]
        [Route("centralapi/parkinglot/{id}/reservation")]
        public async Task<ActionResult<IEnumerable<PrivateParkAPI.DTO.ReservationDTO>>> GetAllNotCanceledReservations(int id) {
            return await _reservationService.GetAllNotCanceledReservations(id);
        }

        //Get Resevations by Id
        [HttpGet]
        [Route("centralapi/parkinglot/{pLotID}/reservation/{id}")]
        public async Task<ActionResult<PrivateParkAPI.DTO.ReservationDTO>> GetReservationById(string id, int pLotID) {
            if (id == null) {
                return NotFound();
            }
            //if (await ReservationExists(id) == false) {
            //    return NotFound("Reservarion not Found");
            //}
            return await _reservationService.GetReservationById(id, pLotID);
            //if (PrivateParkAPI.DTO.ReservationDTO == null) {
            //    return NotFound();
            //}
        }


        //POST Resevation only in ParkAPI
        [HttpPost]
        [Route("centralapi/parkinglot/{pLotID}/apireservation")]
        public async Task<ActionResult<PrivateParkAPI.DTO.ReservationDTO>> PostReservation([FromBody] PrivateParkAPI.DTO.ReservationDTO reservationDTO, int pLotID)
        {
            await _reservationService.PostReservation(reservationDTO, pLotID);
            return CreatedAtAction("PostReservation", new { id = reservationDTO.reservationID }, reservationDTO);
        }

        //POST Reservation in Both Central and Park APIs
        [HttpPost]
        [Route("centralapi/parkinglot/{pLotID}/reservation")]
        public async Task<ActionResult<CentralReservationDTO>> PostUserReservation([FromBody] CentralReservationDTO reservationDTO, int pLotID)
        {            
           await _reservationService.PostUserReservation(reservationDTO, pLotID);
            return CreatedAtAction("PostReservation", new { id = reservationDTO.reservationID }, reservationDTO);
        }

        //POST Cancel Reservation by ID
        [HttpPost]
        [Route("centralapi/parkinglot/{pLotID}/cancelreservation/{id}")]
        public async Task<ActionResult<PrivateParkAPI.DTO.ReservationDTO>> PatchReservation(string id, int pLotID)
        {
            var reservationDTO = await _reservationService.GetReservationById(id, pLotID);

            //if (await ReservationExists(id, pLotID))
            //{
            //    if (reservationDTO.Value.isCancelled == false)
            //    {
                    await _reservationService.PatchReservation(id, pLotID);
                    return Ok(reservationDTO);
            //    }
            //    return BadRequest("Couldn't change value");
            //}
            //return NotFound("Reservation does not Exist");
        }

        //[HttpGet]
        //[Route("centralapi/parkinglot/{pLotid}/reservationexists/{id}")]
        //public async Task<bool> ReservationExists(string id, int pLotID) {
        //    return await _reservationService.FindReservationAny(id, pLotID);
        //}
    }
}
