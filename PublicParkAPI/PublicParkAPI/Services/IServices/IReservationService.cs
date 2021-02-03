using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PublicParkAPI.DTO;
using PublicParkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicParkAPI.Services.IServices
{
    public interface IReservationService
    {
        Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservations();
        Task<ActionResult<ReservationDTO>> GetReservation(string id);
        Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservationsNotCancelled();
        Task<ActionResult<ReservationDTO>> PostReservation(ReservationDTO reservationDTO);
        //GetEndTimeAndFinalPrice
        Task<ActionResult<Reservation>> PatchReservation(string id);
        ValidationResult Validate(ReservationDTO reservationDTO);
    }
}
