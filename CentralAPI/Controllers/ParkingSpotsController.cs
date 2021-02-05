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

        [HttpGet]
        [Route("centralapi/privateparkingspots")]

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

        //GetByIdPrivate
        [HttpGet]
        [Route("centralapi/privateparkingspots/{id}")]
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
        [Route("centralapi/privateparkingspots")]

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
        [Route("centralapi/privateparkingspots/{id}")]

        public async Task<ActionResult<PrivateParkAPI.DTO.ParkingSpotDTO>> Edit(int id, [Bind("parkingSpotID, priceHour, floor, isPrivate, isCovered, parkingLotID")] PrivateParkAPI.DTO.ParkingSpotDTO parkingSpotDTO) {

            using (HttpClient client = new HttpClient()) {

                StringContent content = new StringContent(JsonConvert.SerializeObject(parkingSpotDTO), Encoding.UTF8, "application/json");
                string endpoint = privateApiBaseUrl + "/parkingspots/" + id;
                var response = await client.PutAsync(endpoint, content);
            }
            return parkingSpotDTO;
        }

        [HttpGet]
        [Route("centralapi/publicparkingspots")]
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
        [Route("centralapi/publicparkingspots/{id}")]
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

        //POST public
        [HttpPost]
        [Route("centralapi/publicparkingspots")]

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
        [Route("centralapi/publicparkingspots/{id}")]

        public async Task<ActionResult<PublicParkAPI.DTO.ParkingSpotDTO>> Edit(int id, [Bind("parkingSpotID, priceHour, parkingLotID")] PublicParkAPI.DTO.ParkingSpotDTO parkingSpotDTO) {

            using (HttpClient client = new HttpClient()) {

                StringContent content = new StringContent(JsonConvert.SerializeObject(parkingSpotDTO), Encoding.UTF8, "application/json");
                string endpoint = publicApiBaseUrl + "/parkingspots/" + id;
                var response = await client.PutAsync(endpoint, content);
            }
            return parkingSpotDTO;
        }

    }
}
