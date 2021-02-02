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
        Task<IEnumerable<ReservationDTO>> GetReservations();
        Task<ReservationDTO> GetReservation(string id);
        Task<IEnumerable<ReservationDTO>> GetReservationsNotCancelled();
        Task<ReservationDTO> PostReservation(ReservationDTO reservationDTO);
        //Task<ReservationDTO> PutReservation(string id, ReservationDTO reservationDTO);
        //Task<ReservationDTO> DeleteReservation(string id);
        ValidationResult Validate(ReservationDTO reservationDTO);
    }
}
