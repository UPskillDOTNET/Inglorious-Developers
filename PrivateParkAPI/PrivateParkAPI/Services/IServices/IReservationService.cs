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
        Task<ActionResult<ReservationDTO>> GetReservation(string id);
        Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservationsNotCancelled();
        Task<ActionResult<ReservationDTO>> PostReservation(ReservationDTO reservationDTO);
        //Task<ReservationDTO> PutReservation(string id, ReservationDTO reservationDTO);
        //Task<ReservationDTO> DeleteReservation(string id);
        ValidationResult Validate(ReservationDTO reservationDTO);
    }
}
