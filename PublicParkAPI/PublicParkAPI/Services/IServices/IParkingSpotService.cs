using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PublicParkAPI.DTO;
using PublicParkAPI.Models;
using System;
using System.Collections.Generic;
using FluentValidation.Results;
using System.Linq;
using System.Threading.Tasks;

namespace PublicParkAPI.Services
{
    public interface IParkingSpotService
    {
<<<<<<< HEAD
        Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetAllParkingSpots();
        Task<ActionResult<ParkingSpotDTO>> GetParkingSpot(string id);
        Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetFreeParkingSpots();
        Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetFreeParkingSpotsByDate(DateTime startDate, DateTime endDate);
        Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetParkingPriceFreeSpots(decimal priceHour);
        Task<ActionResult<ParkingSpotDTO>> PutParkingSpot(string id, ParkingSpotDTO parkingSpotDTO);
        Task<ActionResult<ParkingSpotDTO>> PostParkingSpot(ParkingSpotDTO parkingSpotDTO);
        Task<ActionResult<ParkingSpotDTO>> DeleteParkingSpot(string id);
        Task<ParkingSpotDTO> Find(string id);
        Task<bool> FindParkingSpotAny(string id);
=======
        Task<IEnumerable<ParkingSpotDTO>> GetParkingSpots();
        Task<ActionResult<ParkingSpotDTO>> GetParkingSpot(string id);
        Task<IEnumerable<ParkingSpotDTO>> GetFreeParkingSpots();
        Task<ActionResult<ParkingSpotDTO>> PutParkingSpot(string id, ParkingSpotDTO parkingSpotDTO);
        Task<ActionResult<ParkingSpotDTO>> PostParkingSpot(ParkingSpotDTO parkingSpotDTO);
        Task<ActionResult<ParkingSpotDTO>> DeleteParkingSpot(string id);
        Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetParkingSpecificFreeSpots(DateTime entryHour, DateTime leaveHour);
        Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetParkingPriceFreeSpots(decimal price);
        Task<ParkingSpot> GetSpecificParkingSpot(ReservationDTO reservationDTO);
>>>>>>> c6bd7685ce42d6279ba3d7412fcbb0d0779b1b3c
        ValidationResult Validate(ParkingSpotDTO parkingSpotDTO);

    }
}
