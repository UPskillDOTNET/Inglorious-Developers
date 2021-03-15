using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WebApp.DTO;
using WebApp.Services.IServices;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Security.Claims;

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
                ViewData["parkingLotId"] = id;
                ViewBag.parkLotName = _parkingLotService.GetParkingLotById(id).Result.Value.name;
                return View((await _parkingSpotService.GetAllParkingSpots(id)).Value);
            } catch {
                return NotFound();
            }
        }

        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> ManagerSpots() {
            try {
                var managerID = HttpContext.User.FindFirst("sub")?.Value;
                ViewBag.parkLotName = _parkingLotService.GetParkingLotsByManagerId(managerID).Result.Value.name;
                ViewData["parkingLotId"] = _parkingLotService.GetParkingLotsByManagerId(managerID).Result.Value.parkingLotID;
                return View((await _parkingSpotService.GetAllParkingSpotsByManagerID(managerID)).Value);
            } catch {
                return NotFound();
            }
        }
        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> Free(int id) {
            try {
                ViewData["parkingLotId"] = id;
                ViewBag.parkLotName = _parkingLotService.GetParkingLotById(id).Result.Value.name;
                return View((await _parkingSpotService.GetAllFreeParkingSpots(id)).Value);
            } catch (Exception) 
            {
                return NotFound();
            }
        }

        //public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> FreeDate(DateTime startDate, DateTime endDate, int id) {
        //    try {
        //        return await _parkingSpotService.GetFreeParkingSpotsByDate(startDate, endDate, id);
        //    } catch (Exception) 
        //    {
        //        return NotFound();
        //    }
        //}

        public async Task<ActionResult<ParkingSpotDTO>> Details(int id, string pSpotId) {
            try 
            {
                ViewData["parkingSpotId"] = pSpotId;
                ViewBag.parkLotName =(await _parkingLotService.GetParkingLotById(id)).Value.name;
                var parkingSpot =(await _parkingSpotService.GetParkingSpotById(id, pSpotId)).Value;
                parkingSpot.parkingLotID = id;
                return View(parkingSpot);
            } catch {
                return NotFound();
            }
        }

        public async Task<IActionResult> Create() {
            ViewBag.allPlotsNames = (await _parkingLotService.GetAllParkingLots()).Value;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ParkingSpotDTO parkingSpotDTO) {
            try {
                var pLotId = parkingSpotDTO.parkingLotID;
                await _parkingSpotService.CreateParkingSpot(parkingSpotDTO, pLotId);
                var pSpotId = parkingSpotDTO.parkingSpotID;
                var id = pLotId;

                /* Return to ParkingLot Index*/
                return RedirectToAction("ManagerSpots", "ParkingSpots", new { id });

                /* Return to ParkingSpot Details*/
                //return RedirectToAction("Details", "ParkingSpots", new { id, pSpotId } );
            } catch (Exception) {
                return BadRequest();
            }
        }

        public async Task<IActionResult> Edit(int id, string pSpotId) {
            ViewData["parkingLotId"] = id;
            ViewBag.parkLotName = (await _parkingLotService.GetParkingLotById(id)).Value.name;
            ViewData["parkingSpotId"] = pSpotId;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<ParkingSpotDTO>> Edit(int id, ParkingSpotDTO parkingSpotDTO, string pSpotId) {

            try {
                parkingSpotDTO.parkingLotID = id;
                parkingSpotDTO.parkingSpotID = pSpotId;

                //parkingSpotDTO.priceHour.ToString(CultureInfo.InvariantCulture);
                await _parkingSpotService.EditParkingSpot(id, parkingSpotDTO, pSpotId);

                return RedirectToAction("Details", "ParkingSpots", new { id, pSpotId });
            } catch (Exception) {

                return BadRequest();
            }
        }

    }
}
