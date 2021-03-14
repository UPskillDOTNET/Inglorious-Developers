using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DTO;

namespace WebApp.Services.IServices
{
    public interface IParkingLotService
    {
        public Task<ActionResult<IEnumerable<ParkingLotDTO>>> GetAllParkingLots();
        public Task<ActionResult<ParkingLotDTO>> GetParkingLotsByManagerId(string managerID);
        public Task<ActionResult<ParkingLotDTO>> GetParkingLotById(int? pLotId);
    }
}
