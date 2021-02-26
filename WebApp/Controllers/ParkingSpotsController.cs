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
    public class ParkingSpotsController : Controller {

        private readonly IParkingSpotService _parkingSpotService;

        public ParkingSpotsController(IParkingSpotService parkingSpotService) 
        {
            _parkingSpotService = parkingSpotService;
        }

        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> Index(int pLotId) {
            try {
                return View( _parkingSpotService.GetAllParkingSpots(pLotId).Result.Value);
            } catch {
                return NotFound();
            }
        }

        public async Task<ActionResult<ParkingSpotDTO>> Details(string pSpotId) {
            try {
                return View( _parkingSpotService.GetParkingSpotById(pSpotId).Result.Value);
            } catch {
                return NotFound();
            }
        }
    }
}
