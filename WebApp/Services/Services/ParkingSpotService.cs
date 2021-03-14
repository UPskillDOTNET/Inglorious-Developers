using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DTO;
using WebApp.Services.Services.Utils;
using WebApp.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace WebApp.Services.Services {
    public class ParkingSpotService : IParkingSpotService {

        private readonly APIHelper _helper;
        private readonly IParkingLotService _parkingLotService;


        public ParkingSpotService(IParkingLotService parkingLotService, APIHelper helper) 
        {
            _parkingLotService = parkingLotService;
            _helper = helper;
        }

        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetAllParkingSpots(int pLotId) {
            var parkingLot = _parkingLotService.GetParkingLotById(pLotId);
            var response = await _helper.GetClientAsync("central/parkingSpots/parkinglot/" + pLotId);
            return await response.Content.ReadAsAsync<List<ParkingSpotDTO>>();
        }
        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetAllParkingSpotsByManagerID(string managerID) {
            var parkingLot = _parkingLotService.GetParkingLotsByManagerId(managerID).Result.Value;
            var pLotId = parkingLot.parkingLotID;
            var response = await _helper.GetClientAsync("central/parkingSpots/parkinglot/" + pLotId);
            return await response.Content.ReadAsAsync<List<ParkingSpotDTO>>();
        }
        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetAllFreeParkingSpots(int id) {
            var parkingLot = _parkingLotService.GetParkingLotById(id);
            var response = await _helper.GetClientAsync("central/parkingSpots/free/parkinglot/" + id);
            return await response.Content.ReadAsAsync<List<ParkingSpotDTO>>();
        }
        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetFreeParkingSpotsByDate(ReservationDTO reservationDTO) {
            var response = await _helper.GetClientAsync("central/parkingSpots/free/parkinglot/" + reservationDTO.parkingLotID + "/" + reservationDTO.startTime.ToString("yyyy-MM-ddTHH:mm:ss") + "/" + reservationDTO.endTime.ToString("yyyy-MM-ddTHH:mm:ss"));
            return await response.Content.ReadAsAsync<List<ParkingSpotDTO>>();
        }

        public async Task<ActionResult<ParkingSpotDTO>> GetParkingSpotById(int pLotId, string pSpotId) {
            var response = await _helper.GetClientAsync("central/parkingSpots/" + pSpotId + "/parkinglot/" + pLotId);
            return await response.Content.ReadAsAsync<ParkingSpotDTO>();
        }

        public async Task<ActionResult<ParkingSpotDTO>> CreateParkingSpot(ParkingSpotDTO parkingSpotDTO, int pLotId) {
            var content = new StringContent(JsonConvert.SerializeObject(parkingSpotDTO), Encoding.UTF8, "application/json");
            var response = await _helper.PostClientAsync("central/parkingSpots/parkinglot/" + pLotId, content);
            return await response.Content.ReadAsAsync<ParkingSpotDTO>();
        }

        public async Task<ActionResult<ParkingSpotDTO>> EditParkingSpot(int id, ParkingSpotDTO parkingSpotDTO, string pSpotId) {
            var content = new StringContent(JsonConvert.SerializeObject(parkingSpotDTO), Encoding.UTF8, "application/json");
            var response = await _helper.PutClientAsync("central/parkingSpots/parkinglot/" + id + "/parkingspot/" + pSpotId, content);
            return await response.Content.ReadAsAsync<ParkingSpotDTO>();
        }
    }
}
