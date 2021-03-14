using CentralAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Services.IServices
{
    public interface IParkingLotService
    {
        Task<ActionResult<IEnumerable<ParkingLotDTO>>> GetParkingLots();
        Task<ActionResult<ParkingLotDTO>> GetParkingLot(int id);
        Task<ActionResult<ParkingLotDTO>> GetParkingLotsByManagerId(string managerID);
        Task<ActionResult<ParkingLotDTO>> PutParkingLot(int id, ParkingLotDTO parkingLotDTO);
        Task<ActionResult<ParkingLotDTO>> PostParkingLot(ParkingLotDTO parkingLotDTO);

        //ValidationResult Validate(ParkingLotDTO parkingLotDTO);
    }
}
