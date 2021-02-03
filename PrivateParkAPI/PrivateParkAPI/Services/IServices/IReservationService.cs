using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PrivateParkAPI.DTO;
using PrivateParkAPI.Models;

namespace PrivateParkAPI.Services.IServices
{
    public interface IReservationService
    {
        Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservations();
        Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservationsNotCancelled();
        Task<ActionResult<ReservationDTO>> GetReservation(string id);
        Task<ActionResult<ReservationDTO>> PostReservation(ReservationDTO reservationDTO);
        Task<ActionResult<ReservationDTO>> GetEndTimeandFinalPrice(ReservationDTO reservationDTO);
        Task<ActionResult<Reservation>>PatchReservation(string id);
        ValidationResult Validate(ReservationDTO reservationDTO);
    }
}
