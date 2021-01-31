using Microsoft.AspNetCore.Mvc;
using PrivateParkAPI.Models;
using PrivateParkAPI.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<ParkingSpot>> GetProductById()
        {
            return await _parkingSpotService.GetAllnotPrivate();
        }

       
    }
}
