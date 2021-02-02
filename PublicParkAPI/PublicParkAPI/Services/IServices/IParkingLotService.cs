using Microsoft.AspNetCore.Mvc;
using PublicParkAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicParkAPI.Services.IServices
{
    public interface IParkingLotService
    {
        Task<IEnumerable<ParkingLotDTO>> GetParkingLots();
        Task<ParkingLotDTO> GetParkingLot(int id);
        Task<ActionResult<ParkingLotDTO>> PutParkingLot(int id, ParkingLotDTO parkingLotDTO);
        Task<ActionResult<ParkingLotDTO>> PostParkingLot(ParkingLotDTO parkingLotDTO);
    }
}
