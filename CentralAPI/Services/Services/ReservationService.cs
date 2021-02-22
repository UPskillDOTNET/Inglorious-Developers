using CentralAPI.DTO;
using CentralAPI.Services.IServices;
using CentralAPI.Utils;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CentralAPI.Services.Services
{
    public class ReservationService : IReservationService
    {

        private readonly ClientHelper _helper;

        public ReservationService(ClientHelper helper)
        {
            _helper = helper;
        }

        //Method to Get All the reservations from a Parking Lot
        public async Task<ActionResult<IEnumerable<CentralReservationDTO>>> GetAllReservations(int id)
        {

            var response = await _helper.GetClientAsync(id, "api/reservationse");
            try { 
            return await response.Content.ReadAsAsync<List<CentralReservationDTO>>();
            }
            catch (HttpRequestException)
            {
                throw;
            }
        }

        //Method to Get All Reservations that are not cancelled, from a Parking Lot
        public async Task<ActionResult<IEnumerable<CentralReservationDTO>>> GetAllNotCanceledReservations(int id)
        {
            var response = await _helper.GetClientAsync(id, "api/reservations/notCancelled");
            return await response.Content.ReadAsAsync<List<CentralReservationDTO>>();
        }

        //Get a Reservation by ID using Parking Lot ID
        public async Task<ActionResult<CentralReservationDTO>> GetReservationById(string id, int pLotID)
        {

            var endpoint = "api/reservations/" + id;
            var response = await _helper.GetClientAsync(pLotID, endpoint);
            return await response.Content.ReadAsAsync<CentralReservationDTO>();
        }

        //Method to "Cancel" reservations by ID, this method just turn "isCanceled" to true.
        public async Task<ActionResult<HttpResponseMessage>> PatchReservation(string id, int pLotID)
        {
            var content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
            var endpoint = "api/reservations/" + id;
            var response = await _helper.PutClientAsync(pLotID, endpoint, content);
            return response;
        }
        public async Task<ActionResult<HttpResponseMessage>> completeReservation(CentralReservationDTO centralReservationDTO)
        {
            var content = new StringContent(JsonConvert.SerializeObject(centralReservationDTO), Encoding.UTF8, "application/json");
            var endpoint = "api/reservations/notCompleted/" + centralReservationDTO.reservationID;
            var response = await _helper.PutClientAsync(centralReservationDTO.parkingLotID, endpoint, content);
            return response;
        }

        //Method to post a reservation in the Parking Lot API
        public async Task<ActionResult<HttpResponseMessage>> PostReservation(CentralReservationDTO reservationDTO, int pLotID)
        {
            var content = new StringContent(JsonConvert.SerializeObject(reservationDTO), Encoding.UTF8, "application/json");
            var response = await _helper.PostClientAsync(pLotID, "api/reservations/", content);
            return response;
        }

        //public ValidationResult Validate(ReservationDTO reservationDTO)
        //{
        //    ReservationValidator validationRules = new ReservationValidator();
        //    ValidationResult Results = validationRules.Validate(reservationDTO);
        //    return Results;
        //}
    }
}
