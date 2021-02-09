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
        private readonly string privateApiBaseUrl;
        private readonly string publicApiBaseUrl;
        private readonly IConfiguration _configure;
        private readonly ICentralReservationService _centralReservationService;
        private readonly IParkingLotService _parkingLotService;

        public ReservationsController(IConfiguration configuration, ICentralReservationService centralReservationService, IParkingLotService parkingLotService)
        {
            _configure = configuration;
            privateApiBaseUrl = _configure.GetValue<string>("PrivateAPIBaseurl");
            publicApiBaseUrl = _configure.GetValue<string>("PublicAPIBaseurl");
            _centralReservationService = centralReservationService;
            _parkingLotService = parkingLotService;
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
        public async Task<ActionResult<PrivateParkAPI.DTO.ReservationDTO>> PostPrivateReservation([FromBody] PrivateParkAPI.DTO.ReservationDTO reservationDTO)
        {
            using (var client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(reservationDTO), Encoding.UTF8, "application/json");
                string endpoint = privateApiBaseUrl + "/reservations";
                var response = await client.PostAsync(endpoint, content);
            }
            return reservationDTO;
        }

        [HttpPost]
        [Route("centralapi/onereservation")]
        public async Task<ActionResult<CentralReservationDTO>> PostUserReservation([FromBody] CentralReservationDTO reservationDTO)
        {
            PrivateParkAPI.DTO.ReservationDTO privateRes = new PrivateParkAPI.DTO.ReservationDTO();
            privateRes.reservationID = reservationDTO.reservationID;
            privateRes.isCancelled = reservationDTO.isCancelled;
            privateRes.startTime = reservationDTO.startTime;
            privateRes.hours = reservationDTO.hours;
            privateRes.endTime = reservationDTO.endTime;
            privateRes.finalPrice = reservationDTO.finalPrice;
            privateRes.parkingSpotID = reservationDTO.parkingSpotID;

            await PostPrivateReservation(privateRes);
            


            return await _centralReservationService.PostCentralReservation(reservationDTO);

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

        //PUBLIC RESERVATIONS

        //Get All Public Resevations
        [HttpGet]
        [Route("centralapi/allpublicreservation")]
        public async Task<ActionResult<IEnumerable<PublicParkAPI.DTO.ReservationDTO>>> GetAllPublicReservations()
        {
            var reservationList = new List<PublicParkAPI.DTO.ReservationDTO>();
            using (var client = new HttpClient())
            {
                string endpoint = publicApiBaseUrl + "/reservations";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                reservationList = await response.Content.ReadAsAsync<List<PublicParkAPI.DTO.ReservationDTO>>();
            }
            return reservationList;
        }

        //Get All Not Cancelled Reservations
        [HttpGet]
        [Route("centralapi/publicreservation")]
        public async Task<ActionResult<IEnumerable<PublicParkAPI.DTO.ReservationDTO>>> GetAllNotCanceledPublicReservations()
        {
            var reservationList = new List<PublicParkAPI.DTO.ReservationDTO>();
            using (var client = new HttpClient())
            {
                string endpoint = publicApiBaseUrl + "/reservations/notCancelled";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                reservationList = await response.Content.ReadAsAsync<List<PublicParkAPI.DTO.ReservationDTO>>();
            }
            return reservationList;
        }

        //Get Specific Resevations
        [HttpGet]
        [Route("centralapi/publicreservation/{id}")]
        public async Task<ActionResult<PublicParkAPI.DTO.ReservationDTO>> GetSpecificPublicReservation(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PublicParkAPI.DTO.ReservationDTO reservationDTO;
            using (var client = new HttpClient())
            {
                string endpoint = publicApiBaseUrl + "/reservations/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                reservationDTO = await response.Content.ReadAsAsync<PublicParkAPI.DTO.ReservationDTO>();
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
        public async Task<ActionResult<PublicParkAPI.DTO.ReservationDTO>> PostPublicReservation([FromBody] PublicParkAPI.DTO.ReservationDTO reservationDTO)
        {
            using (var client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(reservationDTO), Encoding.UTF8, "application/json");
                string endpoint = publicApiBaseUrl + "/reservations";
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
                string endpoint = publicApiBaseUrl + "/reservations/" + id;
                var response = await client.PatchAsync(endpoint, content);
            }
            return NoContent();
        }
    }
}
