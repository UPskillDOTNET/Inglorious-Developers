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
            if (await ParkingLotExists(id) == false)
            {
                return NotFound("ParkingLot not found");
            }
            return await _parkingLotService.GetParkingLot(id);
        }


        private async Task<bool> ParkingLotExists(int id)
        {
            var parkingLot = await _parkingLotService.GetParkingLot(id);

            if (parkingLot.Value != null)
            {

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
