﻿using System;
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
        Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetAllnotPrivate();
        Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetAllParkingSpots();
        Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetFreeParkingSpots();
        Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetFreeParkingSpotsByDate(DateTime startDate, DateTime endDate);
        Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetFreeParkingSpotsbyPrice(decimal priceHour);
        Task<ActionResult<ParkingSpotDTO>> GetParkingSpot(string id);
        Task <ActionResult<ParkingSpotDTO>> PutParkingSpot(string id, ParkingSpotDTO parkingSpotDTO);
        Task<ActionResult<ParkingSpotDTO>> PostParkingSpot( ParkingSpotDTO parkingSpotDTO);
        Task<ActionResult<ParkingSpotDTO>> DeleteParkingSpot(string id);
        Task<ActionResult<ParkingSpotDTO>> GetSpecificParkingSpot(ReservationDTO reservationDTO);
        ValidationResult Validate(ParkingSpotDTO parkingSpotDTO);
    }
}
