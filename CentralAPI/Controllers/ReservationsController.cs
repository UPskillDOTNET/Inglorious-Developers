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

namespace CentralAPI.Controllers
{
    
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly string privateApiBaseUrl;
        private readonly string publicApiBaseUrl;        
        private readonly IConfiguration _configure;

        public ReservationsController(IConfiguration configuration)
        {
            _configure = configuration;
            privateApiBaseUrl = _configure.GetValue<string>("PrivateAPIBaseurl");
            publicApiBaseUrl = _configure.GetValue<string>("PublicAPIBaseurl");            
        }

        //Get All Private Resevations
        [HttpGet]
        [Route("centralapi/allprivatereservation")]
        public async Task<ActionResult<IEnumerable<PrivateParkAPI.DTO.ReservationDTO>>> GetAllPrivateReservations()
        {
            var reservationList = new List<PrivateParkAPI.DTO.ReservationDTO>();
            using (var client = new HttpClient())
            {
                string endpoint = privateApiBaseUrl + "/reservations";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                reservationList = await response.Content.ReadAsAsync<List<PrivateParkAPI.DTO.ReservationDTO>>();
            }
            return reservationList;
        }

        //Get All Not Cancelled Reservations
        [HttpGet]
        [Route("centralapi/privatereservation")]
        public async Task<ActionResult<IEnumerable<PrivateParkAPI.DTO.ReservationDTO>>> GetAllNotCanceledPrivateReservations()
        {
            var reservationList = new List<PrivateParkAPI.DTO.ReservationDTO>();
            using (var client = new HttpClient())
            {
                string endpoint = privateApiBaseUrl + "/reservations/notCancelled";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                reservationList = await response.Content.ReadAsAsync<List<PrivateParkAPI.DTO.ReservationDTO>>();
            }
            return reservationList;
        }

        //Get Specific Resevations
        [HttpGet]
        [Route("centralapi/privatereservation/{id}")]
        public async Task<ActionResult<PrivateParkAPI.DTO.ReservationDTO>> GetSpecificPrivateReservation(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PrivateParkAPI.DTO.ReservationDTO reservationDTO;
            using (var client = new HttpClient())
            {
                string endpoint = privateApiBaseUrl + "/reservations/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                reservationDTO = await response.Content.ReadAsAsync<PrivateParkAPI.DTO.ReservationDTO>();
            }
            if (reservationDTO == null)
            {
                return NotFound();
            }
            return reservationDTO;
        }


        //POST private reservation
        [HttpPost]
        [Route("centralapi/privatereservation")]
        public async Task<ActionResult<PrivateParkAPI.DTO.ReservationDTO>> PostPrivateReservation([FromBody]PrivateParkAPI.DTO.ReservationDTO reservationDTO)
        {
            using (var client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(reservationDTO), Encoding.UTF8, "application/json");
                string endpoint = privateApiBaseUrl + "/reservations";
                var response = await client.PostAsync(endpoint, content);
            }
            return reservationDTO;
        }

        //POST Cancel Reservation by ID
        [HttpPost]
        [Route("centralapi/cancelprivatereservation/{id}")]
        public async Task<IActionResult> PatchtPrivateReservation(string id)
        {            
            using (var client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
                string endpoint = privateApiBaseUrl + "/reservations/" + id;
                var response = await client.PatchAsync(endpoint, content);               
            }
            return NoContent();
        }

        //Get All Public Resevations
        [HttpGet]
        [Route("centralapi/allpublicrvation")]
        public async Task<ActionResult<IEnumerable<PrivateParkAPI.DTO.ReservationDTO>>> GetAllPublicReservations()
        {
            var reservationList = new List<PrivateParkAPI.DTO.ReservationDTO>();
            using (var client = new HttpClient())
            {
                string endpoint = privateApiBaseUrl + "/reservations";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                reservationList = await response.Content.ReadAsAsync<List<PrivateParkAPI.DTO.ReservationDTO>>();
            }
            return reservationList;
        }

        //Get All Not Cancelled Reservations
        [HttpGet]
        [Route("centralapi/publicreservation")]
        public async Task<ActionResult<IEnumerable<PrivateParkAPI.DTO.ReservationDTO>>> GetAllNotCanceledPublicReservations()
        {
            var reservationList = new List<PrivateParkAPI.DTO.ReservationDTO>();
            using (var client = new HttpClient())
            {
                string endpoint = privateApiBaseUrl + "/reservations/notCancelled";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                reservationList = await response.Content.ReadAsAsync<List<PrivateParkAPI.DTO.ReservationDTO>>();
            }
            return reservationList;
        }

        //Get Specific Resevations
        [HttpGet]
        [Route("centralapi/publicreservation/{id}")]
        public async Task<ActionResult<PrivateParkAPI.DTO.ReservationDTO>> GetSpecificPublicReservation(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PrivateParkAPI.DTO.ReservationDTO reservationDTO;
            using (var client = new HttpClient())
            {
                string endpoint = privateApiBaseUrl + "/reservations/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                reservationDTO = await response.Content.ReadAsAsync<PrivateParkAPI.DTO.ReservationDTO>();
            }
            if (reservationDTO == null)
            {
                return NotFound();
            }
            return reservationDTO;
        }


        //POST private reservation
        [HttpPost]
        [Route("centralapi/publicreservation")]
        public async Task<ActionResult<PrivateParkAPI.DTO.ReservationDTO>> PostPublicReservation([FromBody]PrivateParkAPI.DTO.ReservationDTO reservationDTO)
        {
            using (var client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(reservationDTO), Encoding.UTF8, "application/json");
                string endpoint = privateApiBaseUrl + "/reservations";
                var response = await client.PostAsync(endpoint, content);                
            }
            return reservationDTO;
        }

        //POST Cancel Reservation by ID
        [HttpPost]
        [Route("centralapi/cancelpublicreservation/{id}")]
        public async Task<IActionResult> PatchtPublicReservation(string id)
        {            
            using (var client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
                string endpoint = privateApiBaseUrl + "/reservations/" + id;
                var response = await client.PatchAsync(endpoint, content);
            }
            return NoContent();
        }
    }
}
