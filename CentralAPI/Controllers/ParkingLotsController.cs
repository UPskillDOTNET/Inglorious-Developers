using CentralAPI.DTO;
using CentralAPI.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CentralAPI.Controllers
{
    [Route("centralapi/parkinglots")]
    [ApiController]
    public class ParkingLotsController : Controller
    {
        private readonly IParkingLotService _parkingLotService; 
        
        public ParkingLotsController(IParkingLotService parkingLotService)
        {
            _parkingLotService = parkingLotService;
        }

        //Get All
        [HttpGet]
        public Task<ActionResult<IEnumerable<ParkingLotDTO>>> GetParkingLots()
        {
            return _parkingLotService.GetParkingLots();
        }

        //Get Parking Lot by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingLotDTO>> GetParkingLot(int id)
        {
            try
            {
                return await _parkingLotService.GetParkingLot(id);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Parking Lot "+id+" not found!");
            }
        }


    }
}
