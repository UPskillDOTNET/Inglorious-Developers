using CentralAPI.DTO;
using CentralAPI.Services.IServices;
using CentralAPI.Utils;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PrivateParkAPI.DTO;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CentralAPI.Services.Services
{
    public class ReservationService : IReservationService
    {


        private readonly IParkingLotService _parkingLotService;



        public ReservationService(IParkingLotService parkingLotService)
        {
            _parkingLotService = parkingLotService;
        }

        //Method to Get All the reservations from a Parking Lot
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetAllReservations(int id)
        {

            var reservationList = new List<ReservationDTO>();
            var parkinglot = await _parkingLotService.GetParkingLot(id);

            using (var client = new HttpClient())
            {
                string endpoint = parkinglot.Value.myURL + "/reservations";
                var response = await client.GetAsync(endpoint);
                reservationList = await response.Content.ReadAsAsync<List<ReservationDTO>>();
            }

            return reservationList;
        }

        //Method to Get All Reservations that are not cancelled, from a Parking Lot
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetAllNotCanceledReservations(int id)
        {

            var reservationList = new List<ReservationDTO>();
            var parkinglot = await _parkingLotService.GetParkingLot(id);

            using (var client = new HttpClient())
            {
                string endpoint = parkinglot.Value.myURL + "/reservations/notCancelled";
                var response = await client.GetAsync(endpoint);
                reservationList = await response.Content.ReadAsAsync<List<ReservationDTO>>();
            }

            return reservationList;
        }

        //Get a Reservation by ID using Parking Lot ID
        public async Task<ActionResult<ReservationDTO>> GetReservationById(string id, int pLotID)
        {

            ReservationDTO reservationDTO;
            var parkinglot = await _parkingLotService.GetParkingLot(pLotID);

            using (var client = new HttpClient())
            {
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
        public async Task<ActionResult<CentralReservationDTO>> PostReservation(CentralReservationDTO reservationDTO, int pLotID)
        {

            var parkinglot = await _parkingLotService.GetParkingLot(pLotID);

            using (var client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(reservationDTO), Encoding.UTF8, "application/json");
                string endpoint = parkinglot.Value.myURL + "/reservations";
                var response = await client.PostAsync(endpoint, content);
            }
            return reservationDTO;
        }

        public ValidationResult Validate(ReservationDTO reservationDTO)
        {
            ReservationValidator validationRules = new ReservationValidator();
            ValidationResult Results = validationRules.Validate(reservationDTO);
            return Results;
        }
    }
}
