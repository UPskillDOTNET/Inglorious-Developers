using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp.DTO;

namespace WebApp.Services.IServices
{
    public interface IReservationService
    {
        Task<ActionResult<IEnumerable<ReservationDTO>>> GetAllReservations();
        Task<ActionResult<ReservationDTO>> GetReservationById(string id);
        Task<ActionResult<List<ReservationDTO>>> GetAllReservationsByUser(string id);
        //Task<ActionResult<IEnumerable<ReservationDTO>>> GetCentralReservationsNotCancelled();
        Task<ActionResult<ReservationDTO>> PostCentralReservation(ReservationDTO ReservationDTO);
        //Task<ActionResult<ReservationDTO>> PostCentralReservationNotCompleted(ReservationDTO ReservationDTO);
        //Task<ActionResult<ReservationDTO>> GetEndTimeandFinalPrice(ReservationDTO centralReservation);
        //Task<bool> FindCentralReservationAny(string id);
        //Task<bool> subletReservationExists(ReservationDTO ReservationDTO);
        Task<ActionResult<ReservationDTO>> PatchCentralReservation(string id);
        Task<ActionResult<ReservationDTO>> PutForSublet(string id);
        //Task<ActionResult<ReservationDTO>> CompleteCentralReservation(string id);
        //Task<ActionResult<ReservationDTO>> SubletCentralReservation(string id);
        Task<ActionResult<ReservationDTO>> GetDurationAndFinalPrice(ReservationDTO reservationDTO);
    }
}
