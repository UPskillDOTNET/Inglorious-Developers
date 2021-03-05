using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DTO;
using WebApp.Services.IServices;

namespace WebApp.Controllers
{
    public class ParkingLotsController : Controller
    {
        private readonly IParkingLotService _webParkingLotService;

        public ParkingLotsController(IParkingLotService parkingLotService)
        {
            _webParkingLotService = parkingLotService;
        }
        public async Task<IActionResult> Index(string sortOrder)
        {

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["LocationSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            var parkingLots = _webParkingLotService.GetAllParkingLots().Result.Value;

            switch (sortOrder)
            {
                case "name_desc":
                    parkingLots = parkingLots.OrderByDescending(p => p.name);
                    break;
                default:
                    parkingLots = parkingLots.OrderBy(p => p.name);
                    break;
            }
            try
            {
                return View( _webParkingLotService.GetAllParkingLots().Result.Value);
            }
            catch
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                return View(_webParkingLotService.GetParkingLotById(id).Result.Value);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
