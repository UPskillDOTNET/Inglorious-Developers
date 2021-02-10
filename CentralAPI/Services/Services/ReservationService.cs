using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using PrivateParkAPI.DTO;
using PrivateParkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using CentralAPI.DTO;
using CentralAPI.Services.IServices;
using CentralAPI.Controllers;

namespace CentralAPI.Services.Services {
    public class ReservationService : IReservationService {

        private readonly IMapper _mapper;
        private readonly IConfiguration _configure;
        public ParkingSpotController parkingSpotController;
        private readonly string privateApiBaseUrl;
        private readonly string publicApiBaseUrl;

        public ReservationService(IConfiguration configuration, IMapper mapper) {
            _configure = configuration;
            privateApiBaseUrl = _configure.GetValue<string>("PrivateAPIBaseurl");
            publicApiBaseUrl = _configure.GetValue<string>("PublicAPIBaseurl");
            _mapper = mapper;
        }
        
        public async Task<ActionResult<IEnumerable<PrivateParkAPI.DTO.ReservationDTO>>> GetAllPrivateReservations() {

            var reservationList = new List<PrivateParkAPI.DTO.ReservationDTO>();

            using (var client = new HttpClient()) {
                string endpoint = privateApiBaseUrl + "/reservations";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                reservationList = await response.Content.ReadAsAsync<List<PrivateParkAPI.DTO.ReservationDTO>>();
            }

            return reservationList;
        }


        public async Task<ActionResult<IEnumerable<PrivateParkAPI.DTO.ReservationDTO>>> GetAllNotCanceledPrivateReservations() {

            var reservationList = new List<PrivateParkAPI.DTO.ReservationDTO>();

            using (var client = new HttpClient()) {
                string endpoint = privateApiBaseUrl + "/reservations/notCancelled";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                reservationList = await response.Content.ReadAsAsync<List<PrivateParkAPI.DTO.ReservationDTO>>();
            }

            return reservationList;
        }

        public async Task<ActionResult<PrivateParkAPI.DTO.ReservationDTO>> GetPrivateReservationById(string id) {

            PrivateParkAPI.DTO.ReservationDTO reservationDTO;

            using (var client = new HttpClient()) {
                string endpoint = privateApiBaseUrl + "/reservations/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                reservationDTO = await response.Content.ReadAsAsync<PrivateParkAPI.DTO.ReservationDTO>();
            }

            return reservationDTO;
        }

        public async Task<ActionResult<PrivateParkAPI.DTO.ReservationDTO>> PatchPrivateReservation(string id)
        {
            PrivateParkAPI.DTO.ReservationDTO reservationDTO;

            using (var client = new HttpClient())
            {

                StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
                string endpoint = privateApiBaseUrl + "/reservations/" + id;
                var response = await client.PatchAsync(endpoint, content);
                reservationDTO = await response.Content.ReadAsAsync<PrivateParkAPI.DTO.ReservationDTO>();
            }
            return reservationDTO;
        }
        public async Task<ActionResult<ReservationDTO>> PostReservation(ReservationDTO reservationDTO)
        {
            await GetEndTimeandFinalPrice(reservationDTO);
            var reservation = _mapper.Map<ReservationDTO, Reservation>(reservationDTO);

            using (var client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(reservation), Encoding.UTF8, "application/json");
                string endpoint = privateApiBaseUrl + "/reservations";
                var response = await client.PostAsync(endpoint, content);
            }
            return reservationDTO;
        }

        public async Task<ActionResult<ReservationDTO>> GetEndTimeandFinalPrice(ReservationDTO reservationDTO)
        {
            ParkingSpotController parkingSpotController = new ParkingSpotController(_configure);
            var parkingSpotAction = await parkingSpotController.GetPrivateParkingSpot(reservationDTO.parkingSpotID);
            var parkingSpot = parkingSpotAction.Value;
            reservationDTO.endTime = reservationDTO.startTime.AddHours(reservationDTO.hours);
            reservationDTO.finalPrice = reservationDTO.hours * parkingSpot.priceHour;
            return reservationDTO;
        }

        //[HttpPost]
        //[Route("centralapi/onereservation")]
        //public async Task<ActionResult<CentralReservationDTO>> PostUserReservation([FromBody] CentralReservationDTO reservationDTO) {
        //    PrivateParkAPI.DTO.ReservationDTO privateRes = new PrivateParkAPI.DTO.ReservationDTO();
        //    privateRes.reservationID = reservationDTO.reservationID;
        //    privateRes.isCancelled = reservationDTO.isCancelled;
        //    privateRes.startTime = reservationDTO.startTime;
        //    privateRes.hours = reservationDTO.hours;
        //    privateRes.endTime = reservationDTO.endTime;
        //    privateRes.finalPrice = reservationDTO.finalPrice;
        //    privateRes.parkingSpotID = reservationDTO.parkingSpotID;

        //    await PostReservation(privateRes);



        //    return await _centralReservationController.PostCentralReservation(reservationDTO);

        //}


        //public async Task<ActionResult<Reservation>> PatchReservation(string id) {

        //    return await _reservationRepository.PatchReservation(id);
        //}
        //public async Task<bool> FindReservationAny(string id) {
        //    return await _reservationRepository.FindReservationAny(id);
        //}
        //public ValidationResult Validate(ReservationDTO reservationDTO) {
        //    ReservationValidator validationRules = new ReservationValidator();
        //    ValidationResult Results = validationRules.Validate(reservationDTO);
        //    return Results;
        //}


        public async Task<ActionResult<IEnumerable<PublicParkAPI.DTO.ReservationDTO>>> GetAllPublicReservations() {
            var reservationList = new List<PublicParkAPI.DTO.ReservationDTO>();
            using (var client = new HttpClient()) {
                string endpoint = publicApiBaseUrl + "/reservations";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                reservationList = await response.Content.ReadAsAsync<List<PublicParkAPI.DTO.ReservationDTO>>();
            }
            return reservationList;
        }

        public async Task<ActionResult<IEnumerable<PublicParkAPI.DTO.ReservationDTO>>> GetAllNotCanceledPublicReservations() {
            var reservationList = new List<PublicParkAPI.DTO.ReservationDTO>();
            using (var client = new HttpClient()) {
                string endpoint = publicApiBaseUrl + "/reservations/notCancelled";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                reservationList = await response.Content.ReadAsAsync<List<PublicParkAPI.DTO.ReservationDTO>>();
            }
            return reservationList;
        }

        public async Task<ActionResult<PublicParkAPI.DTO.ReservationDTO>> GetPublicReservationById(string id) {
            PublicParkAPI.DTO.ReservationDTO reservationDTO;
            using (var client = new HttpClient()) {
                string endpoint = publicApiBaseUrl + "/reservations/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                reservationDTO = await response.Content.ReadAsAsync<PublicParkAPI.DTO.ReservationDTO>();
            }
            return reservationDTO;
        }

        public async Task<ActionResult<PublicParkAPI.DTO.ReservationDTO>> PatchPublicReservation(string id)
        {
            PublicParkAPI.DTO.ReservationDTO reservationDTO;
            using (var client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
                string endpoint = publicApiBaseUrl + "/reservations/" + id;
                var response = await client.PatchAsync(endpoint, content);
                reservationDTO = await response.Content.ReadAsAsync<PublicParkAPI.DTO.ReservationDTO>();
            }
            return reservationDTO;
        }
    }
}
