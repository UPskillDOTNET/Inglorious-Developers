using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrivateParkAPI.DTO;
using PrivateParkAPI.Models;
using PrivateParkAPI.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PrivateParkAPI.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestParkingSpotController : Controller
    {
        private readonly IParkingSpotService _parkingSpotService;

        public TestParkingSpotController(IParkingSpotService parkingSpotService)
        {
            _parkingSpotService = parkingSpotService;
        }

        [HttpGet]
        public Task<IEnumerable<ParkingSpotDTO>> GetAllNotPrive()
        {
            return _parkingSpotService.GetAllnotPrivate();
        }

        [HttpGet]
        [Route("~/api/test/all")]
        public Task<IEnumerable<ParkingSpotDTO>> GetAllParkingSpots()
        {
            return _parkingSpotService.GetAllParkingSpots();
        }

        [HttpGet]
        [Route("~/api/test/freeSpots")]
        public Task<IEnumerable<ParkingSpotDTO>> GetAllFreeParkingSpots()
        {
            return _parkingSpotService.GetFreeParkingSpots();
        }


        [HttpGet("{id}")]
        public Task<ParkingSpotDTO> GetParkingSpot(string id)
        {
            var parkingSpots = _parkingSpotService.GetParkingSpot(id);
         
            return parkingSpots;
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> PutParkingSpot(string id, ParkingSpotDTO parkingSpotDTO)
        {
            await _parkingSpotService.PutParkingSpot(id, parkingSpotDTO);

            return NoContent();
        }
    }
}
