using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApp.DTO;
using WebApp.Services.IServices;
using WebApp.Services.Services.Utils;

namespace WebApp.Services.Services
{
    public class ReservationService : IReservationService
    {
        private readonly APIHelper _helper;
        private readonly IParkingLotService _parkingLotService;

        public ReservationService(IParkingLotService parkingLotService, APIHelper helper)
        {
            _parkingLotService = parkingLotService;
            _helper = helper;
        }
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetAllReservations()
        {
            //var parkingLot = _parkingLotService.GetParkingLotById(pLotId);
            var response = await _helper.GetClientAsync("central/reservations");
            return await response.Content.ReadAsAsync<List<ReservationDTO>>();
        }

        public async Task<ActionResult<ReservationDTO>> GetReservationById(string reservationID)
        {
            var response = await _helper.GetClientAsync("central/reservations/" + reservationID);
            return await response.Content.ReadAsAsync<ReservationDTO>();
        }

        public async Task<ActionResult<List<ReservationDTO>>> GetAllReservationsByUser(string id)
        {
            var response = await _helper.GetClientAsync("central/reservations/users/" + id);
            return await response.Content.ReadAsAsync<List<ReservationDTO>>();
        }

        public async Task<ActionResult<ReservationDTO>> PostCentralReservation(ReservationDTO reservationDTO)
        {
            var content = new StringContent(JsonConvert.SerializeObject(reservationDTO), Encoding.UTF8, "application/json");
            var response = await _helper.PostClientAsync("central/reservations", content);
            return await response.Content.ReadAsAsync<ReservationDTO>();
        }

        public async Task<ActionResult<ReservationDTO>> PatchCentralReservation(string id)
        {
            var content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
            var response = await _helper.PutClientAsync("central/reservations" + id, content);
            return await response.Content.ReadAsAsync<ReservationDTO>();
        }
    }
}
