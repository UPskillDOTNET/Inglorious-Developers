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

namespace CentralAPI.Controllers {
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


        //public async Task<ActionResult<IEnumerable<PublicParkAPI.DTO.ParkingSpotDTO><IEnumerable<PrivateParkAPI.DTO.ParkingSpotDTO>> GetAllParkingSpots() {

        //    var parkingSpotsList = new List<PublicParkAPI.DTO.ParkingSpotDTO>();
        //    using (var client = new HttpClient()) {
        //        string endpoint = apiBaseUrl + "/parkingspots/all";
        //        var response = await client.GetAsync(endpoint);
        //        response.EnsureSuccessStatusCode();
        //        parkingSpotsList = await response.Content.ReadAsAsync<List<PublicParkAPI.DTO.ParkingSpotDTO>>();
        //    }
        //    return parkingSpotsList;
        //}
    }
}
