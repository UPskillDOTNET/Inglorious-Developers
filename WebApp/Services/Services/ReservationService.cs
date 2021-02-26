using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
            var response = await _helper.GetClientAsync("central/reservations/");
            return await response.Content.ReadAsAsync<List<ReservationDTO>>();
        }

        public async Task<ActionResult<ReservationDTO>> GetReservationById(string reservationID)
        {
            var response = await _helper.GetClientAsync("central/reservations/" + reservationID);
            return await response.Content.ReadAsAsync<ReservationDTO>();
        }

    }
}
