using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DTO;
using WebApp.Services.IServices;

namespace WebApp.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IReservationService _webReservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _webReservationService = reservationService;
        }
        public async Task<IActionResult> Index()
        {
            //await _webReservationService.GetAllReservations();
            try
            {
                return View(_webReservationService.GetAllReservations().Result.Value);
            }
            catch
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Details(string id)
        {
            try
            {
                return View(_webReservationService.GetReservationById(id).Result.Value);
            }
            catch
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> UserIndex(string id)
        {
            try
            {
                //ViewData["userID"] = id;
                return View(_webReservationService.GetAllReservationsByUser(id).Result.Value);
            }
            catch
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Create(int id , string pSpotId)
        {
            ReservationDTO reservationDTO = new ReservationDTO()
            {
                parkingLotID = id,
                parkingSpotID = pSpotId,
                startTime = DateTime.Now
            };
            return View(reservationDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReservationDTO resevationDTO)
        {
            try
            {
                await _webReservationService.PostCentralReservation(resevationDTO);
                return RedirectToAction("Free", "ParkingSpots", new { id = resevationDTO.parkingLotID });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        
        public async Task<IActionResult> Cancel (string id)
        {
            try
            {
                var cancelReser = _webReservationService.PatchCentralReservation(id).Result.Value;
                return RedirectToAction("Details", "Reservations",new {id});
            } catch
            {
                return BadRequest();
            }
        }
        //public async Task<IActionResult> Edit (string? id)
        //{
        //    try
        //    {
        //        return View(_webReservationService.GetReservationById(id).Result.Value);
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(string id)
        //{
        //    try
        //    {
        //        var cancel = _webReservationService.PatchCentralReservation(id).Result.Value;
        //        return View();
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}
    }
}
