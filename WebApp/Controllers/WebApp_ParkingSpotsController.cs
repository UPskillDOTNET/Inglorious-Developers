using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.DTO;
using WebApp.Services.Services.Utils;
using WebApp.Services.IServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace WebApp.Controllers {
    public class WebApp_ParkingSpotsController : Controller {

        private readonly APIHelper _helper;
        private readonly IWebApp_ParkingSpotService _parkingSpotService;

        public WebApp_ParkingSpotsController(IWebApp_ParkingSpotService parkingSpotService, APIHelper helper) {
            _parkingSpotService = parkingSpotService;
            _helper = helper;
        }

        public async Task<ActionResult<IEnumerable<WebApp_ParkingSpotDTO>>> GetAllParkingSpots(int pLotId) {
            try {
                return View(await _parkingSpotService.GetAllParkingSpots(pLotId));
            } catch (Exception) {
                return NotFound();
            }
        }

        public async Task<ActionResult<WebApp_ParkingSpotDTO>> GetParkingSpotById(string pSpotId, int pLotId) {
            try {
                return View(await _parkingSpotService.GetParkingSpotById(pLotId, pSpotId));
            } catch (Exception) {
                return NotFound();
            }
        }
    }
}
