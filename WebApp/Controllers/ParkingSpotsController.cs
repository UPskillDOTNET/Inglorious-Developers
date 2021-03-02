using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApp.DTO;
using WebApp.Services.IServices;
using System.Threading.Tasks;
using System;

namespace WebApp.Controllers
{
    public class ParkingSpotsController : Controller {

        private readonly IParkingSpotService _parkingSpotService;
        private readonly IParkingLotService _parkingLotService;

        public ParkingSpotsController(IParkingSpotService parkingSpotService, IParkingLotService parkingLotService) 
        {
            _parkingSpotService = parkingSpotService;
            _parkingLotService = parkingLotService;
        }

        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> Index(int id) {
            try {
                ViewBag.parkLotName = _parkingLotService.GetParkingLotById(id).Result.Value.name;
                return View( _parkingSpotService.GetAllParkingSpots(id).Result.Value);
            } catch {
                return NotFound();
            }
        }
        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> Free(int id) {
            try {
                return View( _parkingSpotService.GetAllFreeParkingSpots(id).Result.Value);
            } catch (Exception) 
            {
                return NotFound();
            }
        }

        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> FreeDate(DateTime startDate, DateTime endDate, int id) {
            try {
                return await _parkingSpotService.GetFreeParkingSpotsByDate(startDate, endDate, id);
            } catch (Exception) 
            {
                return NotFound();
            }
        }

        public async Task<ActionResult<ParkingSpotDTO>> Details(int id, string pSpotId) {
            try {
                ViewData["parkingLotId"] = id;
                return View( _parkingSpotService.GetParkingSpotById(id, pSpotId).Result.Value);
            } catch {
                return NotFound();
            }
        }
    }
}
