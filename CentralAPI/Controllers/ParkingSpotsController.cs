using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PrivateParkAPI.DTO;
using PublicParkAPI.DTO;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System;
using Newtonsoft.Json;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CentralAPI.Controllers {
    [ApiController]
    public class ParkingSpotController : ControllerBase {
        private readonly string privateApiBaseUrl;
        private readonly string publicApiBaseUrl;
        private readonly IConfiguration _configure;

        public ParkingSpotController(IConfiguration configuration) {
            _configure = configuration;
            privateApiBaseUrl = _configure.GetValue<string>("PrivateAPIBaseurl");
            publicApiBaseUrl = _configure.GetValue<string>("PublicAPIBaseurl");
        }

        /*------------PRIVATE------------*/

        [HttpGet]
        [Route("centralapi/PriParkingSpots")]

        public async Task<ActionResult<IEnumerable<PrivateParkAPI.DTO.ParkingSpotDTO>>> GetAllPrivateParkingSpots() {

            var parkingSpotsList = new List<PrivateParkAPI.DTO.ParkingSpotDTO>();
            using (var client = new HttpClient()) {
                string endpoint = privateApiBaseUrl + "/parkingspots/all";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                parkingSpotsList = await response.Content.ReadAsAsync<List<PrivateParkAPI.DTO.ParkingSpotDTO>>();
            }
            return parkingSpotsList;
        }

        [HttpGet]
        [Route("centralapi/PriFreeSpots")]
        public async Task<ActionResult<IEnumerable<PrivateParkAPI.DTO.ParkingSpotDTO>>> GetAllPrivateAPIFreeSpots() {
            var parkingSpotsList = new List<PrivateParkAPI.DTO.ParkingSpotDTO>();
            using (var client = new HttpClient()) {
                string endpoint = privateApiBaseUrl + "/parkingspots/freeSpots";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                parkingSpotsList = await response.Content.ReadAsAsync<List<PrivateParkAPI.DTO.ParkingSpotDTO>>();
            }
            return parkingSpotsList;
        }

        [HttpGet]
        [Route("centralapi/PriFreeSpots/{entryHour}/{leaveHour}")]
        public async Task<ActionResult<IEnumerable<PrivateParkAPI.DTO.ParkingSpotDTO>>> GetFreeParkingSpotsByDatePrivate(DateTime startDate, DateTime endDate) {
            if (startDate > endDate) {
                return BadRequest("Dates are not correct");
            }
            var parkingSpotsList = new List<PrivateParkAPI.DTO.ParkingSpotDTO>();
            using (var client = new HttpClient()) {
                string endpoint = privateApiBaseUrl + "/parkingspots/freeSpots/" + startDate.ToString("yyyy-MM-ddTHH:mm:ss") + "/" + endDate.ToString("yyyy-MM-ddTHH:mm:ss");
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                parkingSpotsList = await response.Content.ReadAsAsync<List<PrivateParkAPI.DTO.ParkingSpotDTO>>();
            }
            return parkingSpotsList;
        }

        [HttpGet]
        [Route("centralapi/PriFreeSpots/{priceHour}")]
        public async Task<ActionResult<IEnumerable<PrivateParkAPI.DTO.ParkingSpotDTO>>> GetFreeParkingSpotsByDatePrivate(Decimal priceHour) {
            if (priceHour <= 0) {
                return BadRequest("Can't input a negative price");
            }
            var parkingSpotsList = new List<PrivateParkAPI.DTO.ParkingSpotDTO>();
            using (var client = new HttpClient()) {
                string endpoint = privateApiBaseUrl + "/parkingspots/freeSpots/" + priceHour.ToString(CultureInfo.InvariantCulture);
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                parkingSpotsList = await response.Content.ReadAsAsync<List<PrivateParkAPI.DTO.ParkingSpotDTO>>();
            }
            return parkingSpotsList;
        }

        //GetByIdPrivate
        [HttpGet]
        [Route("centralapi/PriParkingSpots/{id}")]
        public async Task<ActionResult<PrivateParkAPI.DTO.ParkingSpotDTO>> GetPrivateParkingSpot(string id) {
            if (id == null) {
                return NotFound();
            }
            PrivateParkAPI.DTO.ParkingSpotDTO parkingSpot;
            using (var client = new HttpClient()) {

                string endpoint = privateApiBaseUrl + "/parkingspots/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                parkingSpot = await response.Content.ReadAsAsync<PrivateParkAPI.DTO.ParkingSpotDTO>();
            }
            if (parkingSpot == null) {
                return NotFound();
            }
            return parkingSpot;
        }

        //POST private
        [HttpPost]
        [Route("centralapi/PriParkingSpots")]

        public async Task<ActionResult<PrivateParkAPI.DTO.ParkingSpotDTO>> Create([Bind("parkingSpotID, priceHour, floor, isPrivate, isCovered, parkingLotID")] PrivateParkAPI.DTO.ParkingSpotDTO parkingSpotDTO) {

            using (HttpClient client = new HttpClient()) {

                StringContent content = new StringContent(JsonConvert.SerializeObject(parkingSpotDTO), Encoding.UTF8, "application/json");
                string endpoint = privateApiBaseUrl + "/parkingspots";
                var response = await client.PostAsync(endpoint, content);
            }
            return parkingSpotDTO;
        }

        //PUT private
        [HttpPost]
        [Route("centralapi/PriParkingSpots/{id}")]

        public async Task<ActionResult<PrivateParkAPI.DTO.ParkingSpotDTO>> EditPrivateParkingSpot(string id, [Bind("parkingSpotID, priceHour, floor, isPrivate, isCovered, parkingLotID")] PrivateParkAPI.DTO.ParkingSpotDTO parkingSpotDTO) {

            using (HttpClient client = new HttpClient()) {

                StringContent content = new StringContent(JsonConvert.SerializeObject(parkingSpotDTO), Encoding.UTF8, "application/json");
                string endpoint = privateApiBaseUrl + "/parkingspots/" + id;
                var response = await client.PutAsync(endpoint, content);
            }
            return parkingSpotDTO;
        }

        /*------------PUBLIC------------*/

        [HttpGet]
        [Route("centralapi/PubParkingSpots")]
        public async Task<ActionResult<IEnumerable<PublicParkAPI.DTO.ParkingSpotDTO>>> GetAllPublicParkingSpots() {

            var parkingSpotsList = new List<PublicParkAPI.DTO.ParkingSpotDTO>();
            using (var client = new HttpClient()) {
                string endpoint = publicApiBaseUrl + "/parkingSpots";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                parkingSpotsList = await response.Content.ReadAsAsync<List<PublicParkAPI.DTO.ParkingSpotDTO>>();
            }
            return parkingSpotsList;
        }

        [HttpGet]
        [Route("centralapi/PubFreeSpots")]
        public async Task<ActionResult<IEnumerable<PrivateParkAPI.DTO.ParkingSpotDTO>>> GetAllPublicAPIFreeSpots() {
            var parkingSpotsList = new List<PrivateParkAPI.DTO.ParkingSpotDTO>();
            using (var client = new HttpClient()) {
                string endpoint = privateApiBaseUrl + "/parkingSpots/freeSpots";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                parkingSpotsList = await response.Content.ReadAsAsync<List<PrivateParkAPI.DTO.ParkingSpotDTO>>();
            }
            return parkingSpotsList;
        }

        [HttpGet]
        [Route("centralapi/PubParkingSpots/{id}")]
        public async Task<ActionResult<PublicParkAPI.DTO.ParkingSpotDTO>> GetPublicParkingSpot(string id) {
            if (id == null) {
                return NotFound();
            }
            PublicParkAPI.DTO.ParkingSpotDTO parkingSpot;
            using (var client = new HttpClient()) {

                string endpoint = publicApiBaseUrl + "/parkingSpots/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                parkingSpot = await response.Content.ReadAsAsync<PublicParkAPI.DTO.ParkingSpotDTO>();
            }
            if (parkingSpot == null) {
                return NotFound();
            }
            return parkingSpot;
        }

        [HttpGet]
        [Route("centralapi/PubFreeSpots/{entryHour}/{leaveHour}")]
        public async Task<ActionResult<IEnumerable<PrivateParkAPI.DTO.ParkingSpotDTO>>> GetFreeParkingSpotsByDatePublic(DateTime startDate, DateTime endDate) {
            if (startDate > endDate) {
                return BadRequest("Dates are not correct");
            }
            var parkingSpotsList = new List<PrivateParkAPI.DTO.ParkingSpotDTO>();
            using (var client = new HttpClient()) {
                string endpoint = privateApiBaseUrl + "/parkingSpots/freeSpots/" + startDate.ToString("yyyy-MM-ddTHH:mm:ss") + "/" + endDate.ToString("yyyy-MM-ddTHH:mm:ss");
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                parkingSpotsList = await response.Content.ReadAsAsync<List<PrivateParkAPI.DTO.ParkingSpotDTO>>();
            }
            return parkingSpotsList;
        }

        [HttpGet]
        [Route("centralapi/PubFreeSpots/{priceHour}")]
        public async Task<ActionResult<IEnumerable<PrivateParkAPI.DTO.ParkingSpotDTO>>> GetFreeParkingSpotsByDatePublic(Decimal priceHour) {
            if (priceHour <= 0) {
                return BadRequest("Can't input a negative price");
            }
            var parkingSpotsList = new List<PrivateParkAPI.DTO.ParkingSpotDTO>();
            using (var client = new HttpClient()) {
                string endpoint = privateApiBaseUrl + "/parkingSpots/freeSpots/" + priceHour.ToString(CultureInfo.InvariantCulture);
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                parkingSpotsList = await response.Content.ReadAsAsync<List<PrivateParkAPI.DTO.ParkingSpotDTO>>();
            }
            return parkingSpotsList;
        }

        //POST public
        [HttpPost]
        [Route("centralapi/PubParkingSpots")]
        public async Task<ActionResult<PublicParkAPI.DTO.ParkingSpotDTO>> Create([Bind("parkingSpotID, priceHour, parkingLotID")] PublicParkAPI.DTO.ParkingSpotDTO parkingSpotDTO) {

            using (HttpClient client = new HttpClient()) {

                StringContent content = new StringContent(JsonConvert.SerializeObject(parkingSpotDTO), Encoding.UTF8, "application/json");
                string endpoint = publicApiBaseUrl + "/parkingSpots";
                var response = await client.PostAsync(endpoint, content);
            }
            return parkingSpotDTO;
        }

        //PUT public
        [HttpPost]
        [Route("centralapi/PubParkingSpots/{id}")]
        public async Task<ActionResult<PublicParkAPI.DTO.ParkingSpotDTO>> EditPublicParkingSpot(string id, [Bind("parkingSpotID, priceHour, floor, isPrivate, isCovered, parkingLotID")] PublicParkAPI.DTO.ParkingSpotDTO parkingSpotDTO) {

            using (HttpClient client = new HttpClient()) {

                StringContent content = new StringContent(JsonConvert.SerializeObject(parkingSpotDTO), Encoding.UTF8, "application/json");
                string endpoint = publicApiBaseUrl + "/parkingspots/" + id;
                var response = await client.PutAsync(endpoint, content);
            }
            return parkingSpotDTO;
        }
    }
}


