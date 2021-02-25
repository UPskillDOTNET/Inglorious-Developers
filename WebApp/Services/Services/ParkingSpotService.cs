﻿using System;
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


namespace WebApp.Services.Services {
    public class ParkingSpotService : IParkingSpotService {

        private readonly APIHelper _helper;

        public ParkingSpotService(APIHelper helper) {
            _helper = helper;
        }

        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetAllParkingSpots(int pLotId) {
            var response = await _helper.GetClientAsync("parkinglot/" + pLotId);
            return await response.Content.ReadAsAsync<List<ParkingSpotDTO>>();
        }

        public async Task<ActionResult<ParkingSpotDTO>> GetParkingSpotById(int pLotId, string pSpotId) {
            var response = await _helper.GetClientAsync(pSpotId + "/parkinglot/" + pLotId);
            return await response.Content.ReadAsAsync<ParkingSpotDTO>();
        }
    }
}