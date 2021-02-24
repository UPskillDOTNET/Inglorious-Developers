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
    public class WebApp_ParkingLotService : IWebApp_ParkingLotService
    {
        private readonly APIHelper _helper;

        public WebApp_ParkingLotService(APIHelper helper)
        {
            _helper = helper;

        }
        public async Task<ActionResult<IEnumerable<WebApp_ParkingLotDTO>>> GetAllParkingLots()
        {
            var response = await _helper.GetClientAsync("central/parkinglots");
            return await response.Content.ReadAsAsync<List<WebApp_ParkingLotDTO>>();
        }

        public async Task<ActionResult<WebApp_ParkingLotDTO>> GetParkingLotById(string pSpotId)
        {
            var response = await _helper.GetClientAsync("api/parkingspots/" + pSpotId);
            return await response.Content.ReadAsAsync<WebApp_ParkingLotDTO>();
        }
    }
}
