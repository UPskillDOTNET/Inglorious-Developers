using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTO;

namespace WebApp.Services.IServices {
    public interface IWebApp_ParkingSpotService {
        Task<ActionResult<IEnumerable<WebApp_ParkingSpotDTO>>> GetAllParkingSpots(int id);
        //Task<ActionResult<IEnumerable<WebApp_ParkingSpotDTO>>> GetAllFreeParkingSpots(int id);
        //Task<ActionResult<IEnumerable<WebApp_ParkingSpotDTO>>> GetFreeParkingSpotsByDate(DateTime startDate, DateTime endDate, int id);
        Task<ActionResult<WebApp_ParkingSpotDTO>> GetParkingSpotById(int pLotId, string pSpotId);
        //Task<ActionResult<IEnumerable<WebApp_ParkingSpotDTO>>> GetFreeParkingSpotsByPrice(int id, decimal priceHour);
        //Task<ActionResult<WebApp_ParkingSpotDTO>> CreateParkingSpot(WebApp_ParkingSpotDTO parkingSpotDTO, int id);
        //Task<ActionResult<WebApp_ParkingSpotDTO>> EditParkingSpot(string id, WebApp_ParkingSpotDTO parkingSpotDTO, int pLotId);
        //ValidationResult Validate(WebApp_ParkingSpotDTO parkingSpotDTO);
    }
}
