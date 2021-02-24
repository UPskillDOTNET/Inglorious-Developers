using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DTO;
using WebApp.Services.IServices;

namespace WebApp.Controllers
{
    public class WebApp_ParkingLotsController : Controller
    {
        private readonly IWebApp_ParkingLotService _webParkingLotService;

        public WebApp_ParkingLotsController(IWebApp_ParkingLotService parkingLotService)
        {
            _webParkingLotService = parkingLotService;
        }
        public async Task<IActionResult> Index()
        {await _webParkingLotService.GetAllParkingLots();
            try
            {
                return View(await _webParkingLotService.GetAllParkingLots());
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
                return View(await _webParkingLotService.GetParkingLotById(id));
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
