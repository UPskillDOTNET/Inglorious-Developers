using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DTO;
using WebApp.Services.IServices;

namespace WebApp.Controllers
{
    public class MakeReservation : Controller
    {
        private readonly IParkingLotService _webParkingLotService;
        private readonly IParkingSpotService _parkingSpotService;
        private readonly IReservationService _reservationService;

        public MakeReservation(IParkingLotService parkingLotService, IParkingSpotService parkingSpotService, IReservationService reservationService)
        {
            _webParkingLotService = parkingLotService;
            _parkingSpotService = parkingSpotService;
            _reservationService = reservationService;
        }
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["LocationSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
       
            var parkingLots = from p in (await _webParkingLotService.GetAllParkingLots()).Value
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
        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> Now(int id)
        {
            try
            {
                ViewData["parkingLotId"] = id;
                ViewBag.parkLotName = _webParkingLotService.GetParkingLotById(id).Result.Value.name;
                return View(_parkingSpotService.GetAllFreeParkingSpots(id).Result.Value);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }



        public IActionResult Later(int id)
        {
            ReservationDTO reservationDTO = new ReservationDTO()
            {
                parkingLotID = id
            };
            return View(reservationDTO);
        }


        [HttpPost]
        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> Laterr(ReservationDTO reservationDTO)
        {
         
            try
            {
                ViewData["startTime"] = reservationDTO.startTime;
                ViewData["endTime"] = reservationDTO.endTime;
                ViewData["plotID"] = reservationDTO.parkingLotID;
                var vm = await _parkingSpotService.GetFreeParkingSpotsByDate(reservationDTO);
                return View(vm.Value);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        public IActionResult Create(int id, string pSpotId)
        {
            ReservationDTO reservationDTO = new ReservationDTO()
            {
                parkingLotID = id,
                parkingSpotID = pSpotId,
                startTime = DateTime.Now,
                userID = HttpContext.User.FindFirst("sub")?.Value

            };
            return View(reservationDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReservationDTO resevationDTO)
        {
            try
            {
                await _reservationService.PostCentralReservation(resevationDTO);
                
                return RedirectToAction("Free", "ParkingSpots", new { id = resevationDTO.parkingLotID });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        public async Task<IActionResult> CreateLater(int id,  DateTime startDate, DateTime endDate, string pSpotId)
        {
            ReservationDTO reservationDTO = new ReservationDTO()
            {
                parkingLotID = id,
                parkingSpotID = pSpotId,
                startTime = startDate,
                endTime = endDate,
                userID = HttpContext.User.FindFirst("sub")?.Value
            };
            var reservation = await _reservationService.GetDurationAndFinalPrice(reservationDTO);
            reservationDTO = reservation.Value;
            return View(reservationDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLater(ReservationDTO resevationDTO)
        {
            try
            {
                await _reservationService.PostCentralReservation(resevationDTO);
                return RedirectToAction("Free", "ParkingSpots", new { id = resevationDTO.parkingLotID });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
