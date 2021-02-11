using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using PrivateParkAPI.DTO;
using PrivateParkAPI.Models;
using PrivateParkAPI.Repositories;
using PublicParkAPI.Repositories;
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
using FluentValidation.Results;
using CentralAPI.Utils;

namespace CentralAPI.Services.Services {
    public class ReservationService : IReservationService {

        private readonly IMapper _mapper;
        private readonly IConfiguration _configure;
        public readonly ParkingSpotController parkingSpotController;
        private readonly ICentralReservationService _centralReservationService;
        private readonly IParkingLotService _parkingLotService;
        private readonly IParkingSpotService _parkingSpotService;

        //private readonly PrivateParkAPI.Repositories.Repository.ReservationRepository _privateReservationRepository;
        //private readonly PublicParkAPI.Repositories.ReservationRepository _publicReservationRepository;


        public ReservationService(IConfiguration configuration, ICentralReservationService centralReservationService, IParkingLotService parkingLotService, IMapper mapper) {
            _configure = configuration;
            _centralReservationService = centralReservationService;
            _parkingLotService = parkingLotService;            
            _mapper = mapper;
        }
        
        //Method to Get All the reservations from a Parking Lot
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetAllReservations(int id) {

            var reservationList = new List<ReservationDTO>();
            var parkinglot = await _parkingLotService.GetParkingLot(id);

            using (var client = new HttpClient()) {
                string endpoint = parkinglot.Value.myURL + "/reservations";
                var response = await client.GetAsync(endpoint);
                reservationList = await response.Content.ReadAsAsync<List<ReservationDTO>>();
            }

            return reservationList;
        }

        //Method to Get All Reservations that are not cancelled, from a Parking Lot
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetAllNotCanceledReservations(int id) {

            var reservationList = new List<ReservationDTO>();
            var parkinglot = await _parkingLotService.GetParkingLot(id);

            using (var client = new HttpClient()) {
                string endpoint = parkinglot.Value.myURL + "/reservations/notCancelled";
                var response = await client.GetAsync(endpoint); 
                reservationList = await response.Content.ReadAsAsync<List<ReservationDTO>>();
            }

            return reservationList;
        }

        //Get a Reservation by ID using Parking Lot ID
        public async Task<ActionResult<ReservationDTO>> GetReservationById(string id, int pLotID) {

            ReservationDTO reservationDTO;            
            var parkinglot = await _parkingLotService.GetParkingLot(pLotID);

            using (var client = new HttpClient()) {
                string endpoint = parkinglot.Value.myURL + "/reservations/" + id;
                var response = await client.GetAsync(endpoint);
                if (response == null)
                {
                    throw new Exception();
                }
                reservationDTO = await response.Content.ReadAsAsync<ReservationDTO>();
            }

            return reservationDTO;
        }

        //Method to "Cancel" reservations by ID, this method just turn "isCanceled" to true.
        public async Task<ActionResult<ReservationDTO>> PatchReservation(string id, int pLotID)
        {
            ReservationDTO reservationDTO;
            var parkinglot = await _parkingLotService.GetParkingLot(pLotID);

            using (var client = new HttpClient())
            {

                StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
                string endpoint = parkinglot.Value.myURL + "/reservations/" + id;
                var response = await client.PatchAsync(endpoint, content);
                if (response == null)
                {
                    throw new Exception();
                }
                reservationDTO = await response.Content.ReadAsAsync<ReservationDTO>();
            }
            return reservationDTO;
        }

        //Method to post a reservation in the Parking Lot API
        public async Task<ActionResult<ReservationDTO>> PostReservation(ReservationDTO reservationDTO, int pLotID)
        {
            await GetEndTimeAndFinalPrice(reservationDTO, pLotID);
            var reservation = _mapper.Map<ReservationDTO, Reservation>(reservationDTO);            
            var parkinglot = await _parkingLotService.GetParkingLot(pLotID);

            using (var client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(reservation), Encoding.UTF8, "application/json");
                string endpoint = parkinglot.Value.myURL + "/reservations";
                var response = await client.PostAsync(endpoint, content);
            }
            return reservationDTO;
        }

        public async Task<ActionResult<ReservationDTO>> GetEndTimeAndFinalPrice(ReservationDTO reservationDTO, int id)
        {
            ParkingSpotController parkingSpotController = new ParkingSpotController(_configure, _parkingLotService, _parkingSpotService);
            var parkingSpotAction = await parkingSpotController.GetParkingSpotById(id, reservationDTO.parkingSpotID);
            var parkingSpot = parkingSpotAction.Value;
            reservationDTO.endTime = reservationDTO.startTime.AddHours(reservationDTO.hours);
            reservationDTO.finalPrice = reservationDTO.hours * parkingSpot.priceHour;
            return reservationDTO;
        }

       //Method to post a Reservation in CENTRAL and PARKING APIs
        public async Task<ActionResult<CentralReservationDTO>> PostUserReservation([FromBody] CentralReservationDTO reservationDTO, int pLotID)
        {
            ReservationDTO privateRes = new ReservationDTO();
            privateRes.reservationID = reservationDTO.reservationID;
            privateRes.isCancelled = reservationDTO.isCancelled;
            privateRes.startTime = reservationDTO.startTime;
            privateRes.hours = reservationDTO.hours;
            privateRes.endTime = reservationDTO.endTime;
            privateRes.finalPrice = reservationDTO.finalPrice;
            privateRes.parkingSpotID = reservationDTO.parkingSpotID;
            reservationDTO.parkingLotID = pLotID;


            await PostReservation(privateRes, pLotID);
            await _centralReservationService.PostCentralReservation(reservationDTO);


            return reservationDTO;

        }
        public ValidationResult Validate(ReservationDTO reservationDTO) {
            ReservationValidator validationRules = new ReservationValidator();
            ValidationResult Results = validationRules.Validate(reservationDTO);
            return Results;
        }
    }
}
