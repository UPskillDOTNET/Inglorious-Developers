using Microsoft.AspNetCore.Mvc;
using PrivateParkAPI.DTO;
using PrivateParkAPI.Models;
using PrivateParkAPI.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateParkAPI.Controllers
{
    [Route("api/reservationtest")]
    [ApiController]
    public class TestReservationController : Controller
    {
        private readonly IReservationService _reservationService;

        public TestReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public  Task<IEnumerable<ReservationDTO>> GetReservations()
        {
            return  _reservationService.GetReservations();
        }

       
    }
}
