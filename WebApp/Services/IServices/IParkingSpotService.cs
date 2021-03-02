using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTO;

namespace WebApp.Services.IServices {
    public interface IParkingSpotService {
        Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetAllParkingSpots(int pLotId);
        Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetAllFreeParkingSpots(int id);
        Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetFreeParkingSpotsByDate(DateTime startDate, DateTime endDate, int id);
        Task<ActionResult<ParkingSpotDTO>> GetParkingSpotById(int pLotId, string pSpotId);
        //Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetFreeParkingSpotsByPrice(int id, decimal priceHour);
        //Task<ActionResult<ParkingSpotDTO>> CreateParkingSpot(ParkingSpotDTO parkingSpotDTO, int id);
        //Task<ActionResult<ParkingSpotDTO>> EditParkingSpot(string id, ParkingSpotDTO parkingSpotDTO, int pLotId);
        //ValidationResult Validate(ParkingSpotDTO parkingSpotDTO);
    }
}
