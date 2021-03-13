using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DTO;
using WebApp.Services.IServices;

namespace WebApp.Controllers
{
    [Authorize]
    public class ReservationsController : Controller
    {
        private readonly IReservationService _webReservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _webReservationService = reservationService;
        }
        public async Task<IActionResult> Index()
        {
            var id = HttpContext.User.FindFirst("sub")?.Value;
            try
            {
                var vm = await _webReservationService.GetAllReservationsByUser(id);
                return View(vm.Value);
            }
            catch
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Details(string id)
        {
            try
            { var vm = await _webReservationService.GetReservationById(id);
                return View(vm.Value);
            }
            catch
            {
                return NotFound();
            }
        }

        //public IActionResult Create(int id, string pSpotId)
        //{
        //    ReservationDTO reservationDTO = new ReservationDTO()
        //    {
        //        parkingLotID = id,
        //        parkingSpotID = pSpotId,
        //        startTime = DateTime.Now,
        //        userID = HttpContext.User.FindFirst("sub")?.Value
        //    };
        //    return View(reservationDTO);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(ReservationDTO resevationDTO)
        //{
        //    try
        //    {
        //        await _webReservationService.PostCentralReservation(resevationDTO);
        //        return RedirectToAction("Free", "ParkingSpots", new { id = resevationDTO.parkingLotID });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}
        
        public async Task<IActionResult> Cancel (string id)
        {
            try
            {
                await _webReservationService.PatchCentralReservation(id);
                return RedirectToAction("Details", "Reservations",new {id});
            } catch
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> PutForSublet(string id)
        {
            try
            {
                await _webReservationService.PutForSublet(id);
                return RedirectToAction("Details", "Reservations", new { id });
            }
            catch
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
