using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PrivateParkAPI.DTO;
using PrivateParkAPI.Models;

namespace PrivateParkAPI.Services.IServices
{
    public interface IParkingSpotService
    {
        Task<IEnumerable<ParkingSpotDTO>> GetAllnotPrivate();
        Task<IEnumerable<ParkingSpotDTO>> GetAllParkingSpots();
        Task<IEnumerable<ParkingSpotDTO>> GetFreeParkingSpots();
        Task<IEnumerable<ParkingSpotDTO>> GetFreeParkingSpotsByDate(DateTime startDate, DateTime endDate);
        Task<IEnumerable<ParkingSpotDTO>> GetFreeParkingSpotsbyPrice(decimal priceHour);
        Task<ParkingSpotDTO> GetParkingSpot(string id);
        Task <ActionResult<ParkingSpotDTO>> PutParkingSpot(string id, ParkingSpotDTO parkingSpotDTO);
        Task<ActionResult<ParkingSpotDTO>> PostParkingSpot( ParkingSpotDTO parkingSpotDTO);
        Task<ActionResult<ParkingSpotDTO>> DeleteParkingSpot(string id);
        Task<ParkingSpot> GetSpecificParkingSpot(ReservationDTO reservationDTO);
        ValidationResult Validate(ParkingSpotDTO parkingSpotDTO);
    }
}
