using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            await _webReservationService.GetAllReservations();
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
    }
}
