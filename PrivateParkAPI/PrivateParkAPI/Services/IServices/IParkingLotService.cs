using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrivateParkAPI.DTO;
using PrivateParkAPI.Models;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;

namespace PrivateParkAPI.Services.IServices
{
    public interface IParkingLotService
    {
        Task<IEnumerable<ParkingLotDTO>> GetParkingLots();

        Task<ActionResult<ParkingLotDTO>> GetParkingLot(int id);

        Task<ActionResult<ParkingLotDTO>> PutParkingLot(int id, ParkingLotDTO parkingLotDTO);

        Task<ActionResult<ParkingLotDTO>> PostParkingLot(ParkingLotDTO parkingLotDTO);
        ValidationResult Validate(ParkingLotDTO parkingLotDTO);
        //Task<ActionResult<ParkingLotDTO>> DeleteParkingLot(int id);
    }
}
