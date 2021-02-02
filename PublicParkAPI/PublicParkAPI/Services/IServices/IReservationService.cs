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
        Task<IEnumerable<ReservationDTO>> GetReservations();
        Task<ReservationDTO> GetReservation(string id);
        //Task<ReservationDTO> PutReservation(string id, ReservationDTO reservationDTO);
        Task<ActionResult<Reservation>> PatchReservation(string id);
        Task<ReservationDTO> PostReservation(ReservationDTO reservationDTO);
    }
}
