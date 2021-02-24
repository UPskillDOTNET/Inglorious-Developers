using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DTO;

namespace WebApp.Services.IServices
{
    public interface IWebApp_ParkingLotService
    {
        public Task<ActionResult<IEnumerable<WebApp_ParkingLotDTO>>> GetAllParkingLots();
        public Task<ActionResult<WebApp_ParkingLotDTO>> GetParkingLotById(int? pLotId);
    }
}
