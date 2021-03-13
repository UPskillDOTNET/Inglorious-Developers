using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Services.IServices;

namespace WebApp.Controllers
{
    [Authorize]
    public class ParkingLotsController : Controller
    {
        private readonly IParkingLotService _webParkingLotService;

        public ParkingLotsController(IParkingLotService parkingLotService)
        {
            _webParkingLotService = parkingLotService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["LocationSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            var parkingLots = from p in _webParkingLotService.GetAllParkingLots().Result.Value
                              select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                parkingLots = parkingLots.Where(p => p.name.Contains(char.ToUpper(searchString[0]) + searchString.Substring(1))
                                || p.location.Contains(char.ToUpper(searchString[0]) + searchString.Substring(1)));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    parkingLots = parkingLots.OrderByDescending(p => p.name);
                    break;
                default:
                    parkingLots = parkingLots.OrderBy(p => p.name);
                    break;
            }
            
            return View(parkingLots.ToList());
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
