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

        private readonly IParkingLotService _parkingLotService;
        private readonly IReservationService _reservationService;
        private readonly IConfiguration _configure;

        public ReservationsController(IConfiguration configuration, IReservationService reservationService, IParkingLotService parkingLotService)
        {
            _configure = configuration;
            _parkingLotService = parkingLotService;
            _reservationService = reservationService;
        }
       

        //Get All Private Resevations
        [HttpGet]
        [Route("centralapi/allprivatereservation")]
        public async Task<ActionResult<IEnumerable<PrivateParkAPI.DTO.ReservationDTO>>> GetAllPrivateReservations()
        {
            return await _reservationService.GetAllPrivateReservations();
        }


        //Get All Not Cancelled Reservations
        [HttpGet]
        [Route("centralapi/privatereservation")]
        public async Task<ActionResult<IEnumerable<PrivateParkAPI.DTO.ReservationDTO>>> GetAllNotCanceledPrivateReservations() {
            return await _reservationService.GetAllNotCanceledPrivateReservations();
        }

        //Get Private Resevations by Id
        [HttpGet]
        [Route("centralapi/privatereservation/{id}")]
        public async Task<ActionResult<PrivateParkAPI.DTO.ReservationDTO>> GetPrivateReservationById(string id) {
            if (id == null) {
                return NotFound();
            }
            //if (await ReservationExists(id) == false) {
            //    return NotFound("Reservarion not Found");
            //}
            return await _reservationService.GetPrivateReservationById(id);
            //if (PrivateParkAPI.DTO.ReservationDTO == null) {
            //    return NotFound();
            //}
        }


        //POST Private Resevation only in PrivateAPI
        [HttpPost]
        [Route("centralapi/privateapireservation")]
        public async Task<ActionResult<PrivateParkAPI.DTO.ReservationDTO>> PostPrivateReservation([FromBody] PrivateParkAPI.DTO.ReservationDTO reservationDTO)
        {
            await _reservationService.PostReservation(reservationDTO);
            return CreatedAtAction("PostReservation", new { id = reservationDTO.reservationID }, reservationDTO);
        }

        //POST Private Reservation in Both Central and Private APIs
        [HttpPost]
        [Route("centralapi/privatereservation")]
        public async Task<ActionResult<CentralReservationDTO>> PostUserReservation([FromBody] CentralReservationDTO reservationDTO)
        {            
           await _reservationService.PostUserReservation(reservationDTO);
            return CreatedAtAction("PostReservation", new { id = reservationDTO.reservationID }, reservationDTO);
        }

        //POST Cancel Reservation by ID
        [HttpPost]
        [Route("centralapi/cancelprivatereservation/{id}")]
        public async Task<ActionResult<PrivateParkAPI.DTO.ReservationDTO>> PatchPrivateReservation(string id)
        {
            var reservationDTO = await _reservationService.GetPrivateReservationById(id);

            //if (await ReservationExists(id))
            //{
                if (reservationDTO.Value.isCancelled == false)
                {
                    await _reservationService.PatchPrivateReservation(id);
                    return Ok(reservationDTO);
                }
                return BadRequest("Couldn't change value");
           // }
            //return NotFound("Reservation does not Exist");
        }

        //PUBLIC RESERVATIONS

        //Get All Public Resevations
        [HttpGet]
        [Route("centralapi/allpublicreservation")]
        public async Task<ActionResult<IEnumerable<PublicParkAPI.DTO.ReservationDTO>>> GetAllPublicReservations() {
            return await _reservationService.GetAllPublicReservations();
        }

        //Get All Not Cancelled Reservations
        [HttpGet]
        [Route("centralapi/publicreservation")]
        public async Task<ActionResult<IEnumerable<PublicParkAPI.DTO.ReservationDTO>>> GetAllNotCanceledPublicReservations() {
            return await _reservationService.GetAllNotCanceledPublicReservations();
        }

        //Get Specific Resevations
        [HttpGet]
        [Route("centralapi/publicreservation/{id}")]
        public async Task<ActionResult<PublicParkAPI.DTO.ReservationDTO>> GetPublicReservationById(string id) {
            if (id == null) {
                return NotFound();
            }
            //if (await ReservationExists(id) == false) {
            //    return NotFound("Reservarion not Found");
            //}
            return await _reservationService.GetPublicReservationById(id);
            //if (PrivateParkAPI.DTO.ReservationDTO == null) {
            //    return NotFound();
            //}
        }

        ////POST Cancel Reservation by ID
        [HttpPost]
        [Route("centralapi/cancelpublicreservation/{id}")]
        public async Task<ActionResult<PublicParkAPI.DTO.ReservationDTO>> PatchPublicReservation(string id)
        {
            var reservationDTO = await _reservationService.GetPrivateReservationById(id);

            //if (await ReservationExists(id))
            //{
            if (reservationDTO.Value.isCancelled == false)
            {
                await _reservationService.PatchPublicReservation(id);
                return Ok(reservationDTO);
            }
            return BadRequest("Couldn't change value");
            // }
            //return NotFound("Reservation does not Exist");
        }


        ////POST private reservation
        //[HttpPost]
        //[Route("centralapi/publicreservation")]
        //public async Task<ActionResult<PublicParkAPI.DTO.ReservationDTO>> PostPublicReservation([FromBody] PublicParkAPI.DTO.ReservationDTO reservationDTO)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        StringContent content = new StringContent(JsonConvert.SerializeObject(reservationDTO), Encoding.UTF8, "application/json");
        //        string endpoint = publicApiBaseUrl + "/reservations";
        //        var response = await client.PostAsync(endpoint, content);
        //    }
        //    return reservationDTO;
        //}

        ////POST Cancel Reservation by ID
        //[HttpPost]
        //[Route("centralapi/cancelpublicreservation/{id}")]
        //public async Task<IActionResult> PatchtPublicReservation(string id)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
        //        string endpoint = publicApiBaseUrl + "/reservations/" + id;
        //        var response = await client.PatchAsync(endpoint, content);
        //    }
        //    return NoContent();
        //}

        //public async Task<bool> ReservationExists(string id) {
        //    return await _reservationService.GetReservation(id);
        //}
    }
}
