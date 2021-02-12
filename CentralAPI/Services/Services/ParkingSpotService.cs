using CentralAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CentralAPI.Repositories.IRepository;
using Microsoft.Extensions.Configuration;
using FluentValidation.Results;
using CentralAPI.Utils;
using CentralAPI.DTO;

namespace CentralAPI.Services.Services {
    public class ParkingSpotService : IParkingSpotService {

        private readonly IParkingLotService _parkingLotService;

        public ParkingSpotService(IParkingLotService parkingLotService) {
            _parkingLotService = parkingLotService;
        }

        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetAllParkingSpots(int id) 
        {
            var parkingSpotsList = new List<ParkingSpotDTO>();
            var parkinglot = await _parkingLotService.GetParkingLot(id);
            using (var client = new HttpClient()) {
                string endpoint = parkinglot.Value.myURL + "/parkingspots/all";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                parkingSpotsList = await response.Content.ReadAsAsync<List<ParkingSpotDTO>>();
            }
            return parkingSpotsList;
        }

        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetAllFreeSpots(int id) {
            var parkingSpotsList = new List<ParkingSpotDTO>();
            var parkinglot = await _parkingLotService.GetParkingLot(id);
            using (var client = new HttpClient()) {
                string endpoint = parkinglot.Value.myURL + "/parkingspots/freeSpots";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                parkingSpotsList = await response.Content.ReadAsAsync<List<ParkingSpotDTO>>();
            }
            return parkingSpotsList;
        }

        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetFreeParkingSpotsByDate(DateTime startDate, DateTime endDate, int id) {
            var parkingSpotsList = new List<ParkingSpotDTO>();
            var parkinglot = await _parkingLotService.GetParkingLot(id);
            using (var client = new HttpClient()) {
                string endpoint = parkinglot.Value.myURL + "/parkingspots/freeSpots/" + startDate.ToString("yyyy-MM-ddTHH:mm:ss") + "/" + endDate.ToString("yyyy-MM-ddTHH:mm:ss");
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                parkingSpotsList = await response.Content.ReadAsAsync<List<ParkingSpotDTO>>();
            }
            return parkingSpotsList;
        }

        public async Task<ActionResult<ParkingSpotDTO>> GetParkingSpotById(int pLotId, string id) {
            ParkingSpotDTO parkingSpot;
            var parkinglot = await _parkingLotService.GetParkingLot(pLotId);
            using (var client = new HttpClient()) {

                string endpoint = parkinglot.Value.myURL + "/parkingspots/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                parkingSpot = await response.Content.ReadAsAsync<ParkingSpotDTO>();
            }
            return parkingSpot;
        }

        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetFreeParkingSpotsByPrice(int id, decimal priceHour) {
            var parkingSpotsList = new List<ParkingSpotDTO>();
            var parkinglot = await _parkingLotService.GetParkingLot(id);
            using (var client = new HttpClient()) {
                string endpoint = parkinglot.Value.myURL + "/parkingspots/freeSpots/" + priceHour.ToString(CultureInfo.InvariantCulture);
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                parkingSpotsList = await response.Content.ReadAsAsync<List<ParkingSpotDTO>>();
            }
            return parkingSpotsList;
        }
        public async Task<ActionResult<ParkingSpotDTO>> CreateParkingSpot(ParkingSpotDTO parkingSpotDTO, int id) {
            var parkinglot = await _parkingLotService.GetParkingLot(id);
            using (HttpClient client = new HttpClient()) {

                parkingSpotDTO.parkingLotID = id;
                StringContent content = new StringContent(JsonConvert.SerializeObject(parkingSpotDTO), Encoding.UTF8, "application/json");                
                string endpoint = parkinglot.Value.myURL + "/parkingspots";
                var response = await client.PostAsync(endpoint, content);
            }
            return parkingSpotDTO;
        }

        public async Task<ActionResult<ParkingSpotDTO>> EditParkingSpot(string id, ParkingSpotDTO parkingSpotDTO, int pLotId) {
            var parkinglot = await _parkingLotService.GetParkingLot(pLotId);
            using (HttpClient client = new HttpClient()) {

                StringContent content = new StringContent(JsonConvert.SerializeObject(parkingSpotDTO), Encoding.UTF8, "application/json");
                string endpoint = parkinglot.Value.myURL + "/parkingspots/" + id;
                var response = await client.PutAsync(endpoint, content);
            }
            return parkingSpotDTO;
        }

        public ValidationResult Validate(ParkingSpotDTO parkingSpotDTO) {
            ParkingSpotValidator validationRules = new ParkingSpotValidator();
            ValidationResult Results = validationRules.Validate(parkingSpotDTO);
            return Results;
        }
    }
}
