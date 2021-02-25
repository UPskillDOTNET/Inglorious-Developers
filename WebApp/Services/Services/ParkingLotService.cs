using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DTO;
using WebApp.Services.IServices;
using WebApp.Services.Services.Utils;
using Newtonsoft.Json;
using System.Net.Http;



namespace WebApp.Services.Services
{
    public class ParkingLotService : IParkingLotService
    {
        private readonly APIHelper _helper;

        public ParkingLotService(APIHelper helper)
        {
            _helper = helper;

        }
        public async Task<ActionResult<IEnumerable<ParkingLotDTO>>> GetAllParkingLots()
        {
            var response = await _helper.GetClientAsync("central/parkinglots");
            return await response.Content.ReadAsAsync<List<ParkingLotDTO>>();
        }

        public async Task<ActionResult<ParkingLotDTO>> GetParkingLotById(int? pLotId)
        {
            var response = await _helper.GetClientAsync("api/parkingspots/" + pLotId);
            return await response.Content.ReadAsAsync<ParkingLotDTO>();
        }
    }
}
