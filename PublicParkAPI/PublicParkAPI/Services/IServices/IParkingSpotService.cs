using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PublicParkAPI.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PublicParkAPI.Services
{
    public interface IParkingSpotService
    {

        Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetAllParkingSpots();
        Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetFreeParkingSpots();
        Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetFreeParkingSpotsByDate(DateTime startDate, DateTime endDate);
        Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetParkingPriceFreeSpots(decimal priceHour);
        Task<ActionResult<ParkingSpotDTO>> GetParkingSpot(string id);
        Task<ActionResult<ParkingSpotDTO>> PutParkingSpot(string id, ParkingSpotDTO parkingSpotDTO);
        Task<ActionResult<ParkingSpotDTO>> PostParkingSpot(ParkingSpotDTO parkingSpotDTO);
        Task<ActionResult<ParkingSpotDTO>> DeleteParkingSpot(string id);
        Task<ParkingSpotDTO> Find(string id);
        Task<bool> FindParkingSpotAny(string id);
        ValidationResult Validate(ParkingSpotDTO parkingSpotDTO);

    }
}
