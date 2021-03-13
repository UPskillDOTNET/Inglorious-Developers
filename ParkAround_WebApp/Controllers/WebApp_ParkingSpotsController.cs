using Microsoft.AspNetCore.Mvc;
using ParkAround_WebApp.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkAround_WebApp.Controllers {
    public class WebApp_ParkingSpotsController : Controller {

        //private readonly IWebApp_ParkingLotService _parkingLotService;
        private readonly IWebApp_ParkingSpotService _wparkingSpotService;

        public WebApp_ParkingSpotsController (IWebApp_ParkingLotService wparkingLotService, IWebApp_ParkingSpotService wparkingSpotService) {
            _parkingLotService = parkingLotService;
            _parkingSpotService = parkingSpotService;
        }

        public IActionResult Index() {
            return View();
        }
    }
}
