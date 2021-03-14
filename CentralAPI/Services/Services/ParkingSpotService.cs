using CentralAPI.DTO;
using CentralAPI.Repositories.IRepository;
using CentralAPI.Services.IServices;
using CentralAPI.Utils;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace CentralAPI.Services.Services
{
    public class ParkingSpotService : IParkingSpotService
    {
        private readonly ClientHelper _helper;
        private readonly ICentralReservationRepository _centralReservationRepository;

        public ParkingSpotService(ClientHelper helper, ICentralReservationRepository centralReservationRepository)
        {
            _helper = helper;
            _centralReservationRepository = centralReservationRepository;

        }

        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetAllParkingSpots(int pLotId)
        {
            var response = await _helper.GetClientAsync(pLotId, "api/parkingspots/all");
            return await response.Content.ReadAsAsync<List<ParkingSpotDTO>>();
        }

        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetAllParkingSpotsByManagerID(string managerID, int pLotId) {
            var response = await _helper.GetClientAsync(pLotId, "api/parkingspots/all");
            return await response.Content.ReadAsAsync<List<ParkingSpotDTO>>();
        }

        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetAllFreeParkingSpots(int pLotId)
        {
            var response = await _helper.GetClientAsync(pLotId, "api/parkingspots/all");
            var parkingspotList = await response.Content.ReadAsAsync<List<ParkingSpotDTO>>();
            var reservationList = await _centralReservationRepository.GetCentralReservationDateTimeNow();
            var res = from p in parkingspotList where !(from r in reservationList where r.parkingSpotID == p.parkingSpotID select r.parkingSpotID).Contains(p.parkingSpotID) select p;
            return res.ToList();
        }

        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetFreeParkingSpotsByDate(DateTime startDate, DateTime endDate, int pLotId)
        {
            var response = await _helper.GetClientAsync(pLotId, "api/parkingspots/all");
            var parkingspotList = await response.Content.ReadAsAsync<List<ParkingSpotDTO>>();
            var reservationList = await _centralReservationRepository.GetSpecificCentralReservation(startDate, endDate);
            var res = from p in parkingspotList where !(from r in reservationList where r.parkingSpotID == p.parkingSpotID select r.parkingSpotID).Contains(p.parkingSpotID) select p;
            return res.ToList();
        }

        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetFreeParkingSpotsByPrice(int pLotId, decimal priceHour)
        {
            var endpoint = "api/parkingspots/freeSpots/" + priceHour.ToString(CultureInfo.InvariantCulture);
            var response = await _helper.GetClientAsync(pLotId, endpoint);
            return await response.Content.ReadAsAsync<List<ParkingSpotDTO>>();
        }

        public async Task<ActionResult<ParkingSpotDTO>> GetParkingSpotById(int pLotId, string pSpotId)
        {
            var response = await _helper.GetClientAsync(pLotId, "api/parkingspots/" + pSpotId);
            return await response.Content.ReadAsAsync<ParkingSpotDTO>();
        }
        public async Task<ActionResult<ParkingSpotDTO>> CreateParkingSpot(ParkingSpotDTO parkingSpotDTO, int pLotId)
        {
            var content = new StringContent(JsonConvert.SerializeObject(parkingSpotDTO), Encoding.UTF8, "application/json");
            var response = await _helper.PostClientAsync(pLotId, "api/parkingspots/", content);
            return await response.Content.ReadAsAsync<ParkingSpotDTO>();
        }

        public async Task<ActionResult<ParkingSpotDTO>> EditParkingSpot(string pSpotId, ParkingSpotDTO parkingSpotDTO, int pLotId)
        {
            var content = new StringContent(JsonConvert.SerializeObject(parkingSpotDTO), Encoding.UTF8, "application/json");
            var endpoint = "api/parkingspots/" + pSpotId;
            var response = await _helper.PutClientAsync(pLotId, endpoint, content);
            return await response.Content.ReadAsAsync<ParkingSpotDTO>();
        }

        public ValidationResult Validate(ParkingSpotDTO parkingSpotDTO)
        {
            ParkingSpotValidator validationRules = new ParkingSpotValidator();
            ValidationResult Results = validationRules.Validate(parkingSpotDTO);
            return Results;
        }
    }
}
