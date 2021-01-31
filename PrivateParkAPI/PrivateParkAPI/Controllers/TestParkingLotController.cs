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
    [Route("api/testParkingLot")]
    [ApiController]
    public class TestParkingLotController : Controller
    {
        private readonly IParkingLotService _parkingLotService;

        public TestParkingLotController(IParkingLotService parkingLotService)
        {
            _parkingLotService = parkingLotService;
        }

        [HttpGet]
        public Task<IEnumerable<ParkingLotDTO>> GetParkingLots()
        {
            return _parkingLotService.GetParkingLots();
        }


    }
}