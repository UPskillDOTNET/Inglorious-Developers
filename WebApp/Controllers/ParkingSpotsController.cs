﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WebApp.DTO;
using WebApp.Services.IServices;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Web;

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
                return View( _parkingSpotService.GetAllParkingSpots(id).Result.Value);
            } catch {
                return NotFound();
            }
        }
        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> Free(int id) {
            try {
                ViewData["parkingLotId"] = id;
                ViewBag.parkLotName = _parkingLotService.GetParkingLotById(id).Result.Value.name;
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

        public async Task<IActionResult> Create() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ParkingSpotDTO parkingSpotDTO, int pLotId) {
            try {
                pLotId = parkingSpotDTO.parkingLotID;
                await _parkingSpotService.CreateParkingSpot(parkingSpotDTO, pLotId);
                var pSpotId = parkingSpotDTO.parkingSpotID;
                var id = pLotId;

                /* Return to ParkingLot Index*/
                return RedirectToAction("Index", "ParkingSpots", new { id });

                /* Return to ParkingSpot Details*/
                //return RedirectToAction("Details", "ParkingSpots", new { id, pSpotId } );
            } catch (Exception) {
                return BadRequest();
            }
        }
    }
}