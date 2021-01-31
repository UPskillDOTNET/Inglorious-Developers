using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicParkAPI.Data;
using PublicParkAPI.DTO;
using PublicParkAPI.Models;
using PublicParkAPI.Services.IServices;

namespace PublicParkAPI.Controllers
{

    [Route("api/testesReservations")]
    [ApiController]
    public class Reservations2Controller : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public Reservations2Controller(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public Task<IEnumerable<ReservationDTO>> GetReservations()
        {
            return _reservationService.GetReservations();
        }
    }
}
