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
        public IActionResult Index()
        {
            return View();
        }
    }
}
