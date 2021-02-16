using FluentValidation.Results;
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
        Task<bool> FindReservationAny(string id);
        Task<ActionResult<ReservationDTO>> PatchReservation(string id);
        Task<ActionResult<ReservationDTO>> completeReservation(ReservationDTO reservationDTO);
        ValidationResult Validate(ReservationDTO reservationDTO);
    }
}
