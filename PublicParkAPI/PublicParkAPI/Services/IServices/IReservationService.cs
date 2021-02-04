﻿using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PublicParkAPI.DTO;
using PublicParkAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PublicParkAPI.Services.IServices
{
    public interface IReservationService
    {
        Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservations();
        Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservationsNotCancelled();
        Task<ActionResult<ReservationDTO>> GetReservation(string id);
        Task<ActionResult<ReservationDTO>> PostReservation(ReservationDTO reservationDTO);
        Task<ActionResult<ReservationDTO>> GetEndTimeandFinalPrice(ReservationDTO reservation);
        Task<bool> FindReservationAny(string id);
        Task<ActionResult<Reservation>> PatchReservation(string id);
        ValidationResult Validate(ReservationDTO reservationDTO);
    }
}
