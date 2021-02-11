using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PrivateParkAPI.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrivateParkAPI.Services.IServices
{
    public interface IParkingLotService
    {
        Task<ActionResult<IEnumerable<ParkingLotDTO>>> GetParkingLots();
        Task<ActionResult<ParkingLotDTO>> GetParkingLot(int id);
        Task<ActionResult<ParkingLotDTO>> PutParkingLot(ParkingLotDTO parkingLotDTO);
        Task<ActionResult<ParkingLotDTO>> PostParkingLot(ParkingLotDTO parkingLotDTO);
        ValidationResult Validate(ParkingLotDTO parkingLotDTO);
    }
}
