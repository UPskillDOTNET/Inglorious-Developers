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
        public async Task<IActionResult> Index()
        {await _webParkingLotService.GetAllParkingLots();
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
